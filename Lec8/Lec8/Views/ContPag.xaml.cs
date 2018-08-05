using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Lec8.Persistence;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lec8.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContPag : ContentPage
    {
        /**
         * Clase de ejemplo de almacenamiento en SQLiteDb, implementamos la interfaz para manejar los eventos y disparadores.
         * 
         * */        
        public class Recipe : INotifyPropertyChanged
        {
            //Evento para cambio de propiedades en la Lista
            public event PropertyChangedEventHandler PropertyChanged;

            public const string PKColumnName = "RecipeId";

            [PrimaryKey, AutoIncrement, Column(PKColumnName)]
            public int Id { get; set; }

            //[MaxLength(255)]
            //Este campo es auxiliar para el atributo Name.
            public string _Name;
            public string Name {
                get { return _Name; }

                set
                {
                    if (_Name == Name)
                    {
                        return;//Si los dos valores son iguales salimos, no asignamos nada.
                    }
                    else
                    {
                        _Name = value;//Si son diferentes significa que se ha actualizado el valor
                        OnPropertyChanged(_Name);
                    }
                }
            }

            private void OnPropertyChanged([CallerMemberName] string propertyName = null)//CallerMemberName recogera el atributo Name por el contexto de la situación, en caso de ser nullo no fallara.
            {
                //Si PropertyChanged es diferente de Null disparamos un nuevo evento pasandole el valor de la propiedad por parametro (Name)
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

                //Sin acorte de codigo es igual a la linea de  arriba
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
                
            }
        }

        private SQLiteAsyncConnection connection;

        private ObservableCollection<Recipe> _recipes;

        public ContPag ()
		{
            BindingContext = Application.Current;
            InitializeComponent ();
            connection = DependencyService.Get<SQLiteDb>().GetSQLiteAsyncConnection();
            
        }

        protected override async void OnAppearing()
        {
            await connection.CreateTableAsync<Recipe>().ConfigureAwait(false);
            //Creamos la tabla sino existe, recogemos las recetas de la BBDD
            var recipes = await connection.Table<Recipe>().ToListAsync().ConfigureAwait(false);
            //Iniciamos la lista visual y cargamos las recetas de la BBDD en ella.
            _recipes = new ObservableCollection<Recipe>(recipes);
            //Asignamos a la lista visual las recetas de la BBDD.
            recipesListView.ItemsSource = _recipes;
            base.OnAppearing();
        }

        private async void OnAdd(object sender, System.EventArgs e)
        {
            var recipe = new Recipe { Name = "Recipe " + DateTime.Now.Ticks };
            //añadimos a la BBDD la receta.
            await connection.InsertAsync(recipe);
            //Añadimos a la lista visual la receta tambien
            _recipes.Add(recipe);
        }

        private async void OnUpdate(object sender, System.EventArgs e)
        {
            var recipe = _recipes[0];//Aqui iria el objeto que seleccione el usuario.
            recipe.Name += "Updated";//Aqui iria la actualizacion del valor en el TextBox del usuario....

            await connection.UpdateAsync(recipe).ConfigureAwait(false);

            _recipes.Remove(recipe);
            _recipes.Add(recipe);
        }

        private async void OnDelete(object sender, System.EventArgs e)
        {
            var recipe = _recipes[0];//Aqui iria el objeto que seleccione el usuario.

            await connection.DeleteAsync(recipe).ConfigureAwait(false);

            _recipes.Remove(recipe);
        }
    }
}