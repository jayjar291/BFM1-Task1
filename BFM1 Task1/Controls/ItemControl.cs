/*
 * Dev Jared Stapley
 * student ID 002401013
 * Class Software I – C# - C968
 */


using System.Windows;
using System.Windows.Controls;

namespace Task1.UI.Controls
{
    public class ItemControl : Control
    {
        /// <summary>
        /// Item Control for MVVM view model
        /// </summary>
        static ItemControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ItemControl), new FrameworkPropertyMetadata(typeof(ItemControl)));
        }
    }
}
