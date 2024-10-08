using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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

        /// <summary>
        /// Move the data from the model to the viewmodel, using reflection. 
        /// Property names in both objects MUST be the same (both name and type)
        /// </summary>
        /// <typeparam name="TModel">The model's type</typeparam>
        /// <param name="model">The model object the data will be moved from</param>
        public void UpdateFromModel<TModel>(TModel model)
        {
            this.Update<TModel>(model, MVVMDirection.FROM);
        }

        /// <summary>
        /// Move the data from the viewmodel to the model, using reflection. 
        /// Property names in both objects MUST be the same (both name and type)
        /// </summary>
        /// <typeparam name="TModel">The model's type</typeparam>
        /// <param name="model">The model object the data will be moved from</param>
        public void UpdateToModel<TModel>(TModel model)
        {
            this.Update<TModel>(model, MVVMDirection.TO);
        }

        /// <summary>
        /// Update to or from the model based on the specified direction. Property names in both 
        /// objects MUST be the same (both name and type), but properties used just for the view 
        /// model aren't affected/used.
        /// </summary>
        /// <typeparam name="TModel">The model's type</typeparam>
        /// <param name="model">The model object the data will be moved to/from</param>
        /// <param name="direction">The direction in which the update will be performed</param>
        public void Update<TModel>(TModel model, MVVMDirection direction)
        {
            PropertyInfo[] mProperties = model.GetType().GetProperties();
            PropertyInfo[] vmProperties = this.GetType().GetProperties();

            foreach (PropertyInfo mProperty in mProperties)
            {
                PropertyInfo vmProperty = this.GetType().GetProperty(mProperty.Name);
                if (vmProperty != null)
                {
                    if (vmProperty.PropertyType.Equals(mProperty.PropertyType))
                    {
                        if (direction == MVVMDirection.FROM)
                        {
                            vmProperty.SetValue(this, mProperty.GetValue(model));
                        }
                        else
                        {
                            vmProperty.SetValue(model, mProperty.GetValue(this));
                        }
                    }
                    else if (vmProperty.PropertyType.IsGenericType
                        && mProperty.PropertyType.IsGenericType)
                    {
                        Type[] vmDerived = vmProperty.PropertyType.GetGenericArguments();
                        Type[] mDerived = mProperty.PropertyType.GetGenericArguments();
                        Type vmGeneric = vmProperty.PropertyType.GetGenericTypeDefinition();
                        Type mGeneric = mProperty.PropertyType.GetGenericTypeDefinition();
                        if (vmDerived[0].Equals(mDerived[0])
                            && mGeneric.Equals(typeof(List<>))
                            && vmGeneric.Equals(typeof(ObservableCollection<>)))
                        {
                            if (direction == MVVMDirection.FROM)
                            {
                                ConstructorInfo c = vmProperty.PropertyType.GetConstructor(new Type[] { mProperty.PropertyType });
                                object o = c.Invoke(new object[] { mProperty.GetValue(model) });
                                vmProperty.SetValue(this, o);
                            }
                            else
                            {   // To model
                                MethodInfo m = vmProperty.PropertyType.GetMethod("ToList");
                                object o = m.Invoke(vmProperty.GetValue(this), null);
                                mProperty.SetValue(model, o);
                            }
                        }
                    }
                }
            }
        }
    }
}
