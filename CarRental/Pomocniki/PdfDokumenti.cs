using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using MColor = MigraDoc.DocumentObjectModel.Color;

namespace CarRental.Pomocniki
{
    // PDF dokumenti (račun, pogodba o najemu, poročilo) s knjižnico PDFsharp / MigraDoc
    public static class PdfDokumenti
    {
        private static readonly MColor Modra = new MColor(33, 150, 243);
        private static readonly MColor Temna = new MColor(33, 33, 33);
        private static readonly MColor Siva = new MColor(120, 120, 120);
        private static readonly MColor SvetloSiva = new MColor(245, 245, 245);
        private static readonly MColor Crta = new MColor(220, 220, 220);

        private static string T(string besedilo) => Jezik.T(besedilo);
        private static string Evri(decimal znesek) => znesek.ToString("N2", CultureInfo.GetCultureInfo("sl-SI")) + " €";
        private static string Datum(DateTime d) => d.ToString("d.M.yyyy");

        public static string StevilkaRacuna(PodatkiRezervacije p) => $"{p.Rezervacija.DatumOd:yyyy}-{p.Rezervacija.Id:D5}";

        // Vpraša, kam shraniti, ustvari PDF in ponudi odpiranje
        public static void ShraniZDialogom(string privzetoIme, Action<string> ustvari)
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "PDF (*.pdf)|*.pdf";
                dialog.FileName = privzetoIme + ".pdf";
                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    ustvari(dialog.FileName);
                    Cursor.Current = Cursors.Default;
                    UiPomocnik.PonudiOdpiranje(dialog.FileName);
                }
                catch (IOException)
                {
                    Cursor.Current = Cursors.Default;
                    Sporocilo.Napaka("Datoteke ni mogoče shraniti. Je morda odprta v drugem programu?");
                }
                catch (Exception ex)
                {
                    Cursor.Current = Cursors.Default;
                    Sporocilo.Napaka("Napaka pri ustvarjanju PDF: {0}", ex.Message);
                }
            }
        }

        // ================= RAČUN =================
        public static void UstvariRacun(PodatkiRezervacije p, string pot)
        {
            var dokument = NovDokument(T("Račun") + " " + StevilkaRacuna(p));
            var sekcija = NovaSekcija(dokument);

            Glava(sekcija, T("RAČUN"), new[]
            {
                (T("Številka računa:"), StevilkaRacuna(p)),
                (T("Datum izdaje:"), Datum(DateTime.Today)),
                (T("Rezervacija:"), "#" + p.Rezervacija.Id),
            });

            Naslov(sekcija, T("Kupec"));
            Odstavek(sekcija, p.ImeStranke, true);
            if (!string.IsNullOrEmpty(p.Stranka.Naslov)) Odstavek(sekcija, p.Stranka.Naslov);
            if (!string.IsNullOrEmpty(p.Stranka.Email)) Odstavek(sekcija, p.Stranka.Email);
            if (!string.IsNullOrEmpty(p.Stranka.Telefon)) Odstavek(sekcija, p.Stranka.Telefon);

            Naslov(sekcija, T("Postavke"));
            var tabela = Tabela(sekcija, new[] { 7.0, 2.0, 2.8, 2.2, 3.0 },
                new[] { T("Opis"), T("Dni"), T("Cena/dan"), T("Popust"), T("Znesek") });

            string opis = $"{T("Najem vozila")} {p.ImeAvtomobila} ({p.Avtomobil.RegistrskaSt})\n" +
                          $"{Datum(p.Rezervacija.DatumOd)} – {Datum(p.Rezervacija.DatumDo)}";
            Vrstica(tabela, false, opis, p.SteviloDni.ToString(), Evri(p.Avtomobil.CenaNaDan),
                p.Rezervacija.Popust > 0 ? p.Rezervacija.Popust.ToString("0.#") + " %" : "-",
                Evri(p.Rezervacija.SkupnaCena));

            decimal stopnja = Nastavitve.StopnjaDdv;
            decimal osnova = Math.Round(p.Rezervacija.SkupnaCena / (1 + stopnja / 100m), 2);
            decimal ddv = p.Rezervacija.SkupnaCena - osnova;

            sekcija.AddParagraph().Format.SpaceAfter = Unit.FromPoint(6);
            var povzetek = Tabela(sekcija, new[] { 12.0, 5.0 }, null, false);
            if (p.ZnesekPopusta > 0)
            {
                Vrstica(povzetek, false, T("Cena brez popusta"), Evri(p.CenaBrezPopusta));
                Vrstica(povzetek, false, T("Popust zvestobe"), "− " + Evri(p.ZnesekPopusta));
            }
            Vrstica(povzetek, false, T("Osnova za DDV"), Evri(osnova));
            Vrstica(povzetek, false, string.Format(T("DDV ({0} %)"), stopnja.ToString("0.#")), Evri(ddv));
            var skupaj = Vrstica(povzetek, true, T("SKUPAJ ZA PLAČILO"), Evri(p.Rezervacija.SkupnaCena));
            skupaj.Shading.Color = SvetloSiva;
            skupaj.Format.Font.Size = 12;
            foreach (Row v in povzetek.Rows)
                v.Cells[1].Format.Alignment = ParagraphAlignment.Right;

            if (p.Placila.Count > 0)
            {
                Naslov(sekcija, T("Plačila"));
                var placila = Tabela(sekcija, new[] { 4.0, 4.5, 4.5, 4.0 },
                    new[] { T("Datum"), T("Način plačila"), T("Status"), T("Znesek") });
                foreach (var pl in p.Placila)
                    Vrstica(placila, false, Datum(pl.DatumPlacila), T(pl.NacinPlacila ?? ""), T(pl.Status ?? ""), Evri(pl.Znesek));

                decimal odprto = p.Rezervacija.SkupnaCena - p.Placano;
                var stanje = sekcija.AddParagraph();
                stanje.Format.SpaceBefore = Unit.FromPoint(8);
                stanje.Format.Alignment = ParagraphAlignment.Right;
                stanje.AddFormattedText(odprto <= 0
                    ? T("Račun je v celoti plačan.")
                    : string.Format(T("Odprto za plačilo: {0}"), Evri(odprto)), TextFormat.Bold);
            }
            else
            {
                var navodilo = sekcija.AddParagraph(string.Format(
                    T("Znesek nakažite na TRR {0}, sklic SI00 {1}."), Nastavitve.PodjetjeTrr, StevilkaRacuna(p)));
                navodilo.Format.SpaceBefore = Unit.FromPoint(14);
            }

            var hvala = sekcija.AddParagraph(T("Hvala za zaupanje!"));
            hvala.Format.SpaceBefore = Unit.FromCentimeter(1);
            hvala.Format.Font.Italic = true;
            hvala.Format.Font.Color = Siva;

            Izrisi(dokument, pot);
        }

        // ================= POGODBA =================
        public static void UstvariPogodbo(PodatkiRezervacije p, string pot)
        {
            var dokument = NovDokument(T("Pogodba o najemu vozila"));
            var sekcija = NovaSekcija(dokument);

            Glava(sekcija, T("POGODBA O NAJEMU VOZILA"), new[]
            {
                (T("Številka pogodbe:"), "P-" + StevilkaRacuna(p)),
                (T("Datum:"), Datum(DateTime.Today)),
            });

            Naslov(sekcija, T("1. Pogodbeni stranki"));
            var stranki = Tabela(sekcija, new[] { 8.5, 8.5 }, new[] { T("Najemodajalec"), T("Najemnik") });
            Vrstica(stranki, false,
                $"{Nastavitve.PodjetjeIme}\n{Nastavitve.PodjetjeNaslov}\n{T("Davčna številka:")} {Nastavitve.PodjetjeDavcna}\n{Nastavitve.PodjetjeTelefon}",
                $"{p.ImeStranke}\n{p.Stranka.Naslov}\n{T("Vozniško dovoljenje:")} {p.Stranka.StVozniske}\n{p.Stranka.Telefon} {p.Stranka.Email}");

            Naslov(sekcija, T("2. Predmet najema"));
            var vozilo = Tabela(sekcija, new[] { 5.0, 12.0 }, null);
            Vrstica(vozilo, false, T("Vozilo"), $"{p.ImeAvtomobila} ({p.Avtomobil.Letnik})");
            Vrstica(vozilo, false, T("Registrska številka"), p.Avtomobil.RegistrskaSt);
            Vrstica(vozilo, false, T("Kategorija"), T(p.Kategorija ?? "-"));
            Vrstica(vozilo, false, T("Barva / sedeži"), $"{p.Avtomobil.Barva} / {p.Avtomobil.StSedezev}");
            Vrstica(vozilo, false, T("Stanje števca ob prevzemu"), p.Avtomobil.PrevozeniKm > 0 ? p.Avtomobil.PrevozeniKm.ToString("N0") + " km" : "________ km");
            Vrstica(vozilo, false, T("Stanje goriva ob prevzemu"), "[  ] 1/4     [  ] 1/2     [  ] 3/4     [  ] " + T("polno"));

            Naslov(sekcija, T("3. Trajanje najema in cena"));
            var cena = Tabela(sekcija, new[] { 5.0, 12.0 }, null);
            Vrstica(cena, false, T("Prevzem vozila"), Datum(p.Rezervacija.DatumOd));
            Vrstica(cena, false, T("Vrnitev vozila"), Datum(p.Rezervacija.DatumDo));
            Vrstica(cena, false, T("Število dni"), p.SteviloDni.ToString());
            Vrstica(cena, false, T("Cena na dan"), Evri(p.Avtomobil.CenaNaDan));
            if (p.Rezervacija.Popust > 0)
                Vrstica(cena, false, T("Popust zvestobe"), p.Rezervacija.Popust.ToString("0.#") + " %");
            Vrstica(cena, true, T("Skupna cena (z DDV)"), Evri(p.Rezervacija.SkupnaCena));

            Naslov(sekcija, T("4. Splošni pogoji"));
            string[] pogoji =
            {
                T("Najemnik mora biti star najmanj 21 let in imeti veljavno vozniško dovoljenje vsaj 2 leti."),
                T("Najemnik vozilo vrne v enakem stanju in z enako količino goriva, kot ga je prevzel."),
                T("Vožnja izven Evropske unije je dovoljena le s pisnim soglasjem najemodajalca."),
                T("Kajenje in prevoz živali v vozilu nista dovoljena."),
                T("Najemnik odgovarja za prometne prekrške in kazni v času najema."),
                T("Vsako poškodbo ali nesrečo mora najemnik takoj sporočiti najemodajalcu in policiji."),
                T("Za zamudo pri vračilu se zaračuna dodaten dan najema po veljavnem ceniku."),
                T("Vozilo je zavarovano z obveznim in kasko zavarovanjem z odbitno franšizo.")
            };
            for (int i = 0; i < pogoji.Length; i++)
            {
                var odst = sekcija.AddParagraph($"{i + 1}. {pogoji[i]}");
                odst.Format.LeftIndent = Unit.FromCentimeter(0.4);
                odst.Format.SpaceAfter = Unit.FromPoint(3);
                odst.Format.Font.Size = 9;
            }

            var kraj = sekcija.AddParagraph(string.Format(T("Kraj in datum: {0}, {1}"), Nastavitve.PodjetjeKraj, Datum(DateTime.Today)));
            kraj.Format.SpaceBefore = Unit.FromCentimeter(0.6);
            kraj.Format.Font.Color = Siva;

            // Podpisi
            var podpisi = sekcija.AddTable();
            podpisi.Rows.LeftIndent = 0;
            podpisi.AddColumn(Unit.FromCentimeter(8.5));
            podpisi.AddColumn(Unit.FromCentimeter(8.5));
            var prazna = podpisi.AddRow();
            prazna.Height = Unit.FromCentimeter(1.6);
            var crte = podpisi.AddRow();
            crte.Cells[0].AddParagraph("______________________________\n" + T("Najemodajalec"));
            crte.Cells[1].AddParagraph("______________________________\n" + T("Najemnik"));
            crte.Cells[0].Format.Alignment = ParagraphAlignment.Center;
            crte.Cells[1].Format.Alignment = ParagraphAlignment.Center;

            Izrisi(dokument, pot);
        }

        // ================= POROČILO =================
        public static void UstvariPorocilo(string naslov, string obdobje, List<KeyValuePair<string, string>> povzetek,
            List<DataTable> tabele, string pot)
        {
            var dokument = NovDokument(naslov);
            var sekcija = NovaSekcija(dokument);

            Glava(sekcija, naslov.ToUpper(), new[]
            {
                (T("Obdobje:"), obdobje),
                (T("Ustvarjeno:"), DateTime.Now.ToString("d.M.yyyy HH:mm")),
            });

            Naslov(sekcija, T("Povzetek"));
            var tabelaPovzetka = Tabela(sekcija, new[] { 9.0, 8.0 }, null);
            foreach (var par in povzetek)
                Vrstica(tabelaPovzetka, false, par.Key, par.Value).Cells[1].Format.Alignment = ParagraphAlignment.Right;

            foreach (var t in tabele)
            {
                if (t.Columns.Count == 0)
                    continue;

                Naslov(sekcija, t.TableName);
                double sirina = 17.0 / t.Columns.Count;
                var sirine = new double[t.Columns.Count];
                var glave = new string[t.Columns.Count];
                for (int i = 0; i < t.Columns.Count; i++)
                {
                    sirine[i] = sirina;
                    glave[i] = t.Columns[i].ColumnName.Trim();
                }

                var tabela = Tabela(sekcija, sirine, glave);
                tabela.Format.Font.Size = 8.5;
                foreach (DataRow r in t.Rows)
                {
                    var vrednosti = new string[t.Columns.Count];
                    for (int i = 0; i < t.Columns.Count; i++)
                        vrednosti[i] = OblikujVrednost(r[i]);
                    Vrstica(tabela, false, vrednosti);
                }
                if (t.Rows.Count == 0)
                    Vrstica(tabela, false, T("Ni podatkov za izbrano obdobje."));
            }

            Izrisi(dokument, pot);
        }

        private static string OblikujVrednost(object v)
        {
            switch (v)
            {
                case null:
                case DBNull _:
                    return "";
                case decimal d:
                    return d.ToString("N2", CultureInfo.GetCultureInfo("sl-SI"));
                case double d:
                    return d.ToString("N2", CultureInfo.GetCultureInfo("sl-SI"));
                case DateTime dt:
                    return Datum(dt);
                default:
                    return Jezik.T(v.ToString());
            }
        }

        // ================= Skupni gradniki =================
        private static Document NovDokument(string naslov)
        {
            var dokument = new Document();
            dokument.Info.Title = naslov;
            dokument.Info.Author = Nastavitve.PodjetjeIme;

            var normalen = dokument.Styles[StyleNames.Normal];
            normalen.Font.Name = "Segoe UI";
            normalen.Font.Size = 10;
            normalen.Font.Color = Temna;
            return dokument;
        }

        private static Section NovaSekcija(Document dokument)
        {
            var sekcija = dokument.AddSection();
            sekcija.PageSetup.PageWidth = Unit.FromCentimeter(21);
            sekcija.PageSetup.PageHeight = Unit.FromCentimeter(29.7);
            sekcija.PageSetup.LeftMargin = Unit.FromCentimeter(2);
            sekcija.PageSetup.RightMargin = Unit.FromCentimeter(2);
            sekcija.PageSetup.TopMargin = Unit.FromCentimeter(1.8);
            sekcija.PageSetup.BottomMargin = Unit.FromCentimeter(2);

            var noga = sekcija.Footers.Primary.AddParagraph();
            noga.Format.Font.Size = 8;
            noga.Format.Font.Color = Siva;
            noga.Format.Alignment = ParagraphAlignment.Center;
            noga.AddText($"{Nastavitve.PodjetjeIme} · {Nastavitve.PodjetjeNaslov} · {T("ID za DDV:")} {Nastavitve.PodjetjeDavcna} · TRR {Nastavitve.PodjetjeTrr}   |   ");
            noga.AddText(T("Stran") + " ");
            noga.AddPageField();
            noga.AddText(" / ");
            noga.AddNumPagesField();
            return sekcija;
        }

        // Podjetje levo, naslov dokumenta in podatki desno
        private static void Glava(Section sekcija, string naslov, (string oznaka, string vrednost)[] podatki)
        {
            var tabela = sekcija.AddTable();
            tabela.AddColumn(Unit.FromCentimeter(9));
            tabela.AddColumn(Unit.FromCentimeter(8));
            var vrstica = tabela.AddRow();

            var levo = vrstica.Cells[0];
            var ime = levo.AddParagraph();
            ime.AddFormattedText(Nastavitve.PodjetjeIme, TextFormat.Bold).Font.Size = 16;
            ime.Format.Font.Color = Modra;
            foreach (string vrsta in new[] { Nastavitve.PodjetjeNaslov, Nastavitve.PodjetjeTelefon, Nastavitve.PodjetjeEmail })
            {
                var o = levo.AddParagraph(vrsta ?? "");
                o.Format.Font.Size = 9;
                o.Format.Font.Color = Siva;
            }

            var desno = vrstica.Cells[1];
            desno.Format.Alignment = ParagraphAlignment.Right;
            var naslovOdst = desno.AddParagraph();
            naslovOdst.AddFormattedText(naslov, TextFormat.Bold).Font.Size = naslov.Length > 14 ? 15 : 18;
            naslovOdst.Format.SpaceAfter = Unit.FromPoint(4);
            foreach (var (oznaka, vrednost) in podatki)
            {
                var o = desno.AddParagraph();
                o.Format.Font.Size = 9;
                o.AddFormattedText(oznaka + " ", TextFormat.NotBold).Color = Siva;
                o.AddFormattedText(vrednost ?? "", TextFormat.Bold);
            }

            var crta = sekcija.AddParagraph();
            crta.Format.Borders.Bottom.Width = 1.5;
            crta.Format.Borders.Bottom.Color = Modra;
            crta.Format.SpaceAfter = Unit.FromPoint(10);
        }

        private static void Naslov(Section sekcija, string besedilo)
        {
            var p = sekcija.AddParagraph(besedilo.ToUpper());
            p.Format.Font.Size = 9;
            p.Format.Font.Bold = true;
            p.Format.Font.Color = Siva;
            p.Format.SpaceBefore = Unit.FromPoint(14);
            p.Format.SpaceAfter = Unit.FromPoint(5);
        }

        private static void Odstavek(Section sekcija, string besedilo, bool krepko = false)
        {
            var p = sekcija.AddParagraph(besedilo ?? "");
            p.Format.Font.Bold = krepko;
        }

        private static Table Tabela(Section sekcija, double[] sirineCm, string[] glave, bool obrobe = true)
        {
            var tabela = sekcija.AddTable();
            tabela.Rows.LeftIndent = 0;
            if (obrobe)
            {
                tabela.Borders.Color = Crta;
                tabela.Borders.Width = 0.5;
            }
            foreach (double s in sirineCm)
                tabela.AddColumn(Unit.FromCentimeter(s));

            if (glave != null)
            {
                var glava = tabela.AddRow();
                glava.HeadingFormat = true;
                glava.Shading.Color = Temna;
                glava.Format.Font.Color = Colors.White;
                glava.Format.Font.Bold = true;
                glava.TopPadding = Unit.FromPoint(4);
                glava.BottomPadding = Unit.FromPoint(4);
                for (int i = 0; i < glave.Length; i++)
                    glava.Cells[i].AddParagraph(glave[i]);
            }
            return tabela;
        }

        private static Row Vrstica(Table tabela, bool krepko, params string[] vrednosti)
        {
            var vrstica = tabela.AddRow();
            vrstica.TopPadding = Unit.FromPoint(3);
            vrstica.BottomPadding = Unit.FromPoint(3);
            vrstica.Format.Font.Bold = krepko;
            for (int i = 0; i < vrednosti.Length && i < tabela.Columns.Count; i++)
                vrstica.Cells[i].AddParagraph(vrednosti[i] ?? "");
            return vrstica;
        }

        private static void Izrisi(Document dokument, string pot)
        {
            var izris = new PdfDocumentRenderer { Document = dokument };
            izris.RenderDocument();
            izris.PdfDocument.Save(pot);
        }
    }
}
