using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WPF_Azul.Model;

namespace WPF_Azul.ViewModel
{
    public enum MVVMDirection { FROM, TO };

    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void UpdateCollection<TModel>(List<TModel> modelList, ObservableCollection<TModel> ViewModelCollection)
        {
            if (modelList == null || ViewModelCollection == null)
            {
                throw new ArgumentNullException();
            }

            ViewModelCollection.Clear();

            foreach (var item in modelList)
            {
                ViewModelCollection.Add(item);
            }
        }

        public void Update2DCollection<T>(T[,] model2DArray, ObservableCollection<ObservableCollection<T>> ViewModel2DCollection)
        {
            if (model2DArray == null || ViewModel2DCollection == null)
            {
                throw new ArgumentNullException();
            }

            ViewModel2DCollection.Clear();

            int rows = model2DArray.GetLength(0);
            int columns = model2DArray.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                var innerCollection = new ObservableCollection<T>();

                for (int j = 0; j < columns; j++)
                {
                    innerCollection.Add(model2DArray[i, j]);
                }

                ViewModel2DCollection.Add(innerCollection);
            }
        }

        public static void UpdateJaggedCollection<T>(List<List<T>> modelJagged2DArray, ObservableCollection<ObservableCollection<T>> ViewModel2DCollection)
        {
            if (modelJagged2DArray == null || ViewModel2DCollection == null)
            {
                throw new ArgumentNullException();
            }

            ViewModel2DCollection.Clear();

            foreach (var row in modelJagged2DArray)
            {
                var innerCollection = new ObservableCollection<T>();

                foreach (var item in row)
                {
                    innerCollection.Add(item);
                }

                ViewModel2DCollection.Add(innerCollection);
            }
        }
    }
}
