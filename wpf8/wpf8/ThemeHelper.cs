using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace wpf8
{
    class ThemeHelper
    {
        private static readonly string[] _themePath = {
                "ColorsDef.xaml",
                "ColorsDark.xaml"
                };
        public static string Current
        {
            get => Properties.Settings.Default.ThemePath == ""
            ? _themePath[0]
            : Properties.Settings.Default.ThemePath;
            set
            {
                Properties.Settings.Default.ThemePath = value;
                Properties.Settings.Default.Save();
            }
        }
        public static void Apply(string themePath)
        {
            var newTheme = new ResourceDictionary
            {
                Source = new Uri(themePath, UriKind.Relative)
            };
            var oldTheme = Application.Current.Resources.MergedDictionaries
            .FirstOrDefault(d => _themePath.Any(path =>
            d.Source != null && d.Source.OriginalString.EndsWith(path,
            StringComparison.OrdinalIgnoreCase)));
            if (oldTheme != null)
            {
                int index =
                Application.Current.Resources.MergedDictionaries.IndexOf(oldTheme);
                Application.Current.Resources.MergedDictionaries[index] =
                newTheme;
            }
            else
            {
                Application.Current.Resources.MergedDictionaries.Add(newTheme);
            }
            Current = themePath;
        }

        public static void ApplySaved()
        {
            var theme = Current;
            Apply(theme);
        }
        public static void Dark()
        {
            var newTheme = _themePath[1];
            Apply(newTheme);
        }
        public static void Def()
        {
            var newTheme = _themePath[0];
            Apply(newTheme);
        }

    }
}
    
