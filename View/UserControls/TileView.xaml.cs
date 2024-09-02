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

namespace WPF_Azul.View.UserControls
{
    /// <summary>
    /// Interaction logic for TileView.xaml
    /// </summary>
    public partial class TileView : UserControl
    {

        #region TileColour DP

        /// <summary>
        /// Gets or sets the background colour of the tile
        /// </summary>
        public Color TileColour
        {
            get { return (Color)GetValue(ValueProperty); }
            set
            {
                SetValue(ValueProperty, value);
            }
        }

        /// <summary>
        /// Identified the BackgroundColour dependency property
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("TileColour", typeof(Color),
              typeof(TileView), new PropertyMetadata(null));

        #endregion

        public TileView()
        {
            InitializeComponent();
            TileViewUI.DataContext = this;
        }

        //protected override void OnMouseMove(MouseEventArgs e)
        //{
        //    base.OnMouseMove(e);
        //    if (e.LeftButton == MouseButtonState.Pressed)
        //    {
        //        // Package the data.
        //        DataObject data = new DataObject();
        //        data.SetData(DataFormats.StringFormat, rectangleUI.Fill.ToString());
        //        data.SetData("Double", rectangleUI.Height);
        //        data.SetData("Object", this);

        //        // Initiate the drag-and-drop operation.
        //        DragDrop.DoDragDrop(this, data, DragDropEffects.Copy | DragDropEffects.Move);
        //    }
        //}

        //protected override void OnGiveFeedback(GiveFeedbackEventArgs e)
        //{
        //    base.OnGiveFeedback(e);
        //    // These Effects values are set in the drop target's
        //    // DragOver event handler.
        //    if (e.Effects.HasFlag(DragDropEffects.Copy))
        //    {
        //        Mouse.SetCursor(Cursors.Cross);
        //    }
        //    else if (e.Effects.HasFlag(DragDropEffects.Move))
        //    {
        //        Mouse.SetCursor(Cursors.Hand);
        //    }
        //    else
        //    {
        //        Mouse.SetCursor(Cursors.No);
        //    }
        //    e.Handled = true;
        //}

        //protected override void OnDrop(DragEventArgs e)
        //{
        //    base.OnDrop(e);

        //    // If the DataObject contains string data, extract it.
        //    if (e.Data.GetDataPresent(DataFormats.StringFormat))
        //    {
        //        string dataString = (string)e.Data.GetData(DataFormats.StringFormat);

        //        // If the string can be converted into a Brush,
        //        // convert it and apply it to the ellipse.
        //        BrushConverter converter = new BrushConverter();
        //        if (converter.IsValid(dataString))
        //        {
        //            Brush newFill = (Brush)converter.ConvertFromString(dataString);
        //            rectangleUI.Fill = newFill;

        //            // Set Effects to notify the drag source what effect
        //            // the drag-and-drop operation had.
        //            // (Copy if CTRL is pressed; otherwise, move.)
        //            if (e.KeyStates.HasFlag(DragDropKeyStates.ControlKey))
        //            {
        //                e.Effects = DragDropEffects.Copy;
        //            }
        //            else
        //            {
        //                e.Effects = DragDropEffects.Move;
        //            }
        //        }
        //    }
        //    e.Handled = true;
        //}

        //protected override void OnDragOver(DragEventArgs e)
        //{
        //    base.OnDragOver(e);
        //    e.Effects = DragDropEffects.None;

        //    // If the DataObject contains string data, extract it.
        //    if (e.Data.GetDataPresent(DataFormats.StringFormat))
        //    {
        //        string dataString = (string)e.Data.GetData(DataFormats.StringFormat);

        //        // If the string can be converted into a Brush, allow copying or moving.
        //        BrushConverter converter = new BrushConverter();
        //        if (converter.IsValid(dataString))
        //        {
        //            // Set Effects to notify the drag source what effect
        //            // the drag-and-drop operation will have. These values are
        //            // used by the drag source's GiveFeedback event handler.
        //            // (Copy if CTRL is pressed; otherwise, move.)
        //            if (e.KeyStates.HasFlag(DragDropKeyStates.ControlKey))
        //            {
        //                e.Effects = DragDropEffects.Copy;
        //            }
        //            else
        //            {
        //                e.Effects = DragDropEffects.Move;
        //            }
        //        }
        //    }
        //    e.Handled = true;
        //}

        //protected override void OnDragEnter(DragEventArgs e)
        //{
        //    base.OnDragEnter(e);
        //    // Save the current Fill brush so that you can revert back to this value in DragLeave.
        //    _previousFill = rectangleUI.Fill;

        //    // If the DataObject contains string data, extract it.
        //    if (e.Data.GetDataPresent(DataFormats.StringFormat))
        //    {
        //        string dataString = (string)e.Data.GetData(DataFormats.StringFormat);

        //        // If the string can be converted into a Brush, convert it.
        //        BrushConverter converter = new BrushConverter();
        //        if (converter.IsValid(dataString))
        //        {
        //            Brush newFill = (Brush)converter.ConvertFromString(dataString.ToString());
        //            rectangleUI.Fill = newFill;
        //        }
        //    }
        //}

        //protected override void OnDragLeave(DragEventArgs e)
        //{
        //    base.OnDragLeave(e);
        //    // Undo the preview that was applied in OnDragEnter.
        //    rectangleUI.Fill = _previousFill;
        //}
    }
}
