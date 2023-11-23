﻿using LiveChartsCore;

namespace ChartWinForms
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            LiveCharts.Configure(config =>
               config
                   // you can override the theme 
                   // .AddDarkTheme()  

                   // In case you need a non-Latin based font, you must register a typeface for SkiaSharp
                   //.HasGlobalSKTypeface(SKFontManager.Default.MatchCharacter('汉')) // <- Chinese 
                   //.HasGlobalSKTypeface(SKFontManager.Default.MatchCharacter('あ')) // <- Japanese 
                   //.HasGlobalSKTypeface(SKFontManager.Default.MatchCharacter('헬')) // <- Korean 
                   //.HasGlobalSKTypeface(SKFontManager.Default.MatchCharacter('Ж'))  // <- Russian 

                   //.HasGlobalSKTypeface(SKFontManager.Default.MatchCharacter('أ'))  // <- Arabic 
                   //.UseRightToLeftSettings() // Enables right to left tooltips 

                   // finally register your own mappers
                   // you can learn more about mappers at:
                   // https://livecharts.dev/docs/winforms/2.0.0-rc1/Overview.Mappers
                   .HasMap<City>((city, point) =>
                   {
                       // here we use the index as X, and the population as Y 
                       point.Coordinate = new(point.Index, city.Population);
                   })
           // .HasMap<Foo>( .... ) 
           // .HasMap<Bar>( .... ) 
           );

            _ = Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        public record City(string Name, double Population);
    }
}