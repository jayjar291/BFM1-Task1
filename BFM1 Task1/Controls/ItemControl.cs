using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Task1.UI.Controls
{
    public class ItemControl : Control
    {
        /// <summary>
        /// Item Control for MVVM
        /// </summary>
        static ItemControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ItemControl), new FrameworkPropertyMetadata(typeof(ItemControl)));
        }
    }
}
