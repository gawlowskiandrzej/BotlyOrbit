using BotlyOrbit.Game.Entities;
using BotlyOrbit.Game.Objects;
using BotlyOrbit.GUI.Helpers;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace BotlyOrbit.GUI.ViewModels
{
    internal class EntitiesViewModel : INotifyPropertyChanged
    {
        private EntityList _entityList;
        public ICommand DoubleClickCommand { get; set; }
        public event EventHandler<Entity> EntityDoubleClicked;


        public EntityList EntityList { get => _entityList; set { _entityList = value; OnPropertyChanged(); } }
        public EntitiesViewModel()
        {
            EntityList = new EntityList();
        }
        public EntitiesViewModel(EntityList entities)
        {
            EntityList = entities;
            DoubleClickCommand = new RelayCommand<object>(OnRowDoubleClicked);
        }

        private void OnRowDoubleClicked(object selectedElement)
        {
            if (selectedElement is Entity entity)
                EntityDoubleClicked?.Invoke(this, entity);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected internal void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
