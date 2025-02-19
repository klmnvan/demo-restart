using DemoEkzZachet.Models;
using MsBox.Avalonia;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

namespace DemoEkzZachet.ViewModels
{
    public class AddTourVM : ViewModelBase
    {
        Tour _tour = new Tour(); 
        List<TypeEntity> _types = new List<TypeEntity>(); //типы для комбобокс
        TypeEntity _type;
        List<ToursType> _toursType = new List<ToursType>(); //типы для выбранных, связь

        public AddTourVM()
        {
            Types = MainWindowViewModel.Context.Types.ToList(); //получаем список всех типов
            _type = Types.FirstOrDefault(); //инициализируем первый
        }

        public Tour Tour
        {
            get => _tour;
            set => this.RaiseAndSetIfChanged(ref _tour, value);
        }

        public TypeEntity SelectType
        {
            get => _type;
            set 
            {
                Tour.ToursTypes.Add(new ToursType { Tour = this.Tour, Type = value }); 
                ToursTypes = Tour.ToursTypes.ToList();
                this.RaiseAndSetIfChanged(ref _type, value);
            }
        }

        public void DeleteType(ToursType dType)
        {
            Tour.ToursTypes.Remove(Tour.ToursTypes.FirstOrDefault(it => it == dType));
            ToursTypes = Tour.ToursTypes.ToList();
            this.RaisePropertyChanged(nameof(ToursTypes));
        }

        public List<TypeEntity> Types 
        { 
            get => _types; 
            set => this.RaiseAndSetIfChanged(ref _types, value); 
        }

        public List<ToursType> ToursTypes
        {
            get => _toursType;
            set => this.RaiseAndSetIfChanged(ref _toursType, value);
        }

        public async void Save()
        {
            if(Tour.Id == 0)
            {
                MainWindowViewModel.Context.Tours.Add(Tour);
                MainWindowViewModel.Context.SaveChanges();
                await MessageBoxManager.GetMessageBoxStandard("а", "добавли", MsBox.Avalonia.Enums.ButtonEnum.Ok).ShowAsync();
                MainWindowViewModel.Instance.CurrentPage = new ListProductView();
            }
        }
    }
}
