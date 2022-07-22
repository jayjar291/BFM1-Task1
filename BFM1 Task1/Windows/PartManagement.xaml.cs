/*
 * Dev Jared Stapley
 * student ID 002401013
 * Class Software I – C# - C968
 */


using System;
using System.Windows;
using System.Windows.Controls;
using Task1.Core;

namespace Task1.UI.Windows
{
    /// <summary>
    /// Interaction logic for PartManagement.xaml
    /// </summary>
    public partial class PartManagement : Window
    {
        /// <summary>
        /// State stores the state of the window. used for saving and cancle functions.
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// Part stores the target part for modifaction, creation.
        /// </summary>
        public Part Part { get; set; }
        /// <summary>
        /// private bool keeps track of the type of window
        /// </summary>
        bool Type;
        /// <summary>
        /// this refreshes the datacontext
        /// </summary>
        private void refresh()
        {
            DataContext = null;
            DataContext = Part;
        }
        /// <summary>
        /// constructor for the PartManagment window
        /// </summary>
        /// <param name="type">type of window "True" for adding parts. "False" for modifying parts</param>
        public PartManagement(bool type = true)
        {
            InitializeComponent();
            //set the window type
            Type = type;
            if (!type)
            {
                this.Title = "Modify Part";
                txtName.Content = this.Title;
            }
            //set the window state
            State = -1;
        }
        /// <summary>
        /// event handler for the radio buttons
        /// will create or modifiy the Part to match the selected type.
        /// also sets the special feild text.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoOutsourced_IsEnabledChanged(object sender, RoutedEventArgs e)
        {
            //check window type
            if (Type)
            {
                //Add Part
                if (rdoOutsourced.IsChecked.Value)
                {
                    //set special feild text and create new part;
                    txtSpecial.Content = "Company Name:";
                    Part = new Outsourced("", 0.0M, 1, 1, 2, "Company Name");
                }
                else
                {
                    //set special feild text and create new part;
                    txtSpecial.Content = "Machine ID";
                    Part = new Inhouse("", 0.0M, 1, 1, 2, 000);
                }
            }
            else
            {
                //Modify part
                if (rdoOutsourced.IsChecked.Value)
                {
                    //set special feild text
                    txtSpecial.Content = "Company Name:";
                    //Clone part to new part type
                    switch (Part)
                    {
                        case Inhouse:
                            Part = new Outsourced(Part.Name, Part.Price, Part.InStock, Part.Min, Part.Max, "", Part.PartID);
                            break;
                        case Outsourced:
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    //set special feild text
                    txtSpecial.Content = "Machine ID";
                    //Clone part to new part type
                    switch (Part)
                    {
                        case Inhouse:
                            break;
                        case Outsourced:
                            Part = new Inhouse(Part.Name, Part.Price, Part.InStock, Part.Min, Part.Max, 0000, Part.PartID);
                            break;
                        default:
                            break;
                    }
                }

            }
            //refresh the dataContext
            refresh();
        }
        /// <summary>
        /// event handeler for the cancle button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancle_Click(object sender, RoutedEventArgs e)
        {
            //set the state and exit
            State = -1;
            Close();
        }
        /// <summary>
        /// event handler for the save button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //set state
            State = 1;
            //data validation
            try
            {
                //get WPF validation errors
                if (Validation.GetHasError(txtMin))
                {
                    throw new Exception("Minimum " + Validation.GetErrors(txtMin)[0].ErrorContent.ToString().Replace('.', ' ') + "to a number.");
                }
                if (Validation.GetHasError(txtMax))
                {
                    throw new Exception("Maximum " + Validation.GetErrors(txtMax)[0].ErrorContent.ToString().Replace('.',' ') + "to a number.");
                }
                if (Validation.GetHasError(txtPrice))
                {
                    throw new Exception("Price/Cost " + Validation.GetErrors(txtPrice)[0].ErrorContent.ToString().Replace('.', ' ') + "to a decimal.");
                }
                if (Validation.GetHasError(txtStock))
                {
                    throw new Exception("Inventory " + Validation.GetErrors(txtStock)[0].ErrorContent.ToString().Replace('.', ' ') + "to a number.");
                }
                if (Validation.GetHasError(txtSpecial1))
                {
                    throw new Exception(txtSpecial.Content + " " + Validation.GetErrors(txtSpecial1)[0].ErrorContent.ToString().Replace('.', ' ') + "to a number.");
                };
                //valadate inputs
                if (int.Parse(txtMin.Text) < 1)
                {
                    throw new Exception("Minimum must be grater than 0");
                }
                if (int.Parse(txtMax.Text) < int.Parse(txtMin.Text))
                {
                    throw new Exception("Maximum must be grater than Minmum");
                }
                if (int.Parse(txtPrice.Text) < 0)
                {
                    throw new Exception("Price/Cost must be grater than 0");
                }
                if (int.Parse(txtStock.Text) >= int.Parse(txtMin.Text) && int.Parse(txtStock.Text) <= int.Parse(txtMax.Text)) { }
                else
                {
                    throw new Exception("Inventory must be between Minimum and Maximum");
                }              
            }
            //reset state and display input error message
            catch (Exception ex)
            {
                State = -1;
                MessageBox.Show($"{ex.Message}", "Input Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            //check that the state has not changed and close
            if (State == 1)
            {
                Close();
            }
        }
        /// <summary>
        /// event handler for window load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //refresh the data context
            refresh();
            //set the selected radio button
            switch (Part)
            {
                case Inhouse:
                    rdoInhouse.IsChecked = true;
                    break;
                case Outsourced:
                    rdoOutsourced.IsChecked = true;
                    break;
                default:
                    break;
            }

        }
    }
}
