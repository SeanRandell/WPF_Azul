using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPF_Azul.View;

namespace WPF_Azul
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //private void btnModal_Click(object sender, RoutedEventArgs e)
        //{
        //    PlayerNameModal modalWindow = new PlayerNameModal(this);
        //    Opacity = 0.4;
        //    modalWindow.ShowDialog();
        //    Opacity = 1;
        //    if (modalWindow.Success)
        //    {
        //        txtblckPlayerName.Text = modalWindow.Input;
        //    }
        //}

        //private void panel_DragOver(object sender, DragEventArgs e)
        //{
        //    if (e.Data.GetDataPresent("Object"))
        //    {
        //        // These Effects values are used in the drag source's
        //        // GiveFeedback event handler to determine which cursor to display.
        //        if (e.KeyStates == DragDropKeyStates.ControlKey)
        //        {
        //            e.Effects = DragDropEffects.Copy;
        //        }
        //        else
        //        {
        //            e.Effects = DragDropEffects.Move;
        //        }
        //    }
        //}

        //private void panel_Drop(object sender, DragEventArgs e)
        //{
        //    // If an element in the panel has already handled the drop,
        //    // the panel should not also handle it.
        //    if (e.Handled == false)
        //    {
        //        Panel _panel = (Panel)sender;
        //        UIElement _element = (UIElement)e.Data.GetData("Object");

        //        if (_panel != null && _element != null)
        //        {
        //            // Get the panel that the element currently belongs to,
        //            // then remove it from that panel and add it the Children of
        //            // the panel that its been dropped on.
        //            Panel _parent = (Panel)VisualTreeHelper.GetParent(_element);

        //            if (_parent != null)
        //            {
        //                if (e.KeyStates == DragDropKeyStates.ControlKey &&
        //                    e.AllowedEffects.HasFlag(DragDropEffects.Copy))
        //                {
        //                    //Circle _circle = new Circle((Circle)_element);
        //                    //_panel.Children.Add(_circle);
        //                    // set the value to return to the DoDragDrop call
        //                    e.Effects = DragDropEffects.Copy;
        //                }
        //                else if (e.AllowedEffects.HasFlag(DragDropEffects.Move))
        //                {
        //                    _parent.Children.Remove(_element);
        //                    _panel.Children.Add(_element);
        //                    // set the value to return to the DoDragDrop call
        //                    e.Effects = DragDropEffects.Move;
        //                }
        //            }
        //        }
        //    }
        //}
    }
}