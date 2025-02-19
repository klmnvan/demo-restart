using DemoEkzZachet.Models;
using Microsoft.EntityFrameworkCore;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DemoEkzZachet.ViewModels
{
    public class ProductEditorVM : ViewModelBase
    {

        private Tour _currentTour = new Tour(); //
        List<TypeEntity> _types = new List<TypeEntity>();
        TypeEntity _currentTourType = new TypeEntity();
        List<ToursType> _currentToursTypes = new List<ToursType>();

        public ProductEditorVM()
        {
            InitToursTypes();
            _currentTourType = Types.FirstOrDefault();
            CurrentTour = new Tour();
            CurrentToursTypes = new List<ToursType>();
        }

        public ProductEditorVM(Tour Tour)
        {
            CurrentTour = Tour;
            InitToursTypes();
            CurrentToursTypes = Tour.ToursTypes.ToList();
            CurrentTourType = CurrentToursTypes.FirstOrDefault().Type;
        }

        void InitToursTypes()
        {
            Types = MainWindowViewModel.Context.Types.ToList();
        }

        public Tour CurrentTour 
        { 
            get => _currentTour; 
            set => _currentTour = value; 
        }
        public List<TypeEntity> Types 
        { 
            get => _types; 
            set => this.RaiseAndSetIfChanged(ref _types, value); 
        }
        public TypeEntity CurrentTourType 
        { 
            get => _currentTourType; 
            set
            {
                if(value.TypeName != "")
                {
                    CurrentTour.ToursTypes.Add(new ToursType { Type = value, Tour = CurrentTour });
                    CurrentToursTypes = CurrentTour.ToursTypes.ToList();
                    this.RaiseAndSetIfChanged(ref _currentTourType, value);
                }
            }
        }

        public List<ToursType> CurrentToursTypes 
        { 
            get => _currentToursTypes; 
            set => this.RaiseAndSetIfChanged(ref _currentToursTypes, value);
        }

        public void DeleteType(ToursType t)
        {
            CurrentTour.ToursTypes.Remove(CurrentTour.ToursTypes.FirstOrDefault(it => it == t));
            CurrentToursTypes = CurrentTour.ToursTypes.ToList();
            this.RaisePropertyChanged(nameof(CurrentToursTypes));
        }

        public void GoBack()
        {
            MainWindowViewModel.Instance.CurrentPage = new ListProductView();
        }

        public async void Save()
        {
            if(CurrentTour.Id == 0)
            {
                MainWindowViewModel.Context.Tours.Add(CurrentTour);
                MainWindowViewModel.Context.SaveChanges();
                await MessageBoxManager.GetMessageBoxStandard("Сообщение", "добавлен", ButtonEnum.Ok).ShowAsync();
            }
            else
            {
                MainWindowViewModel.Context.SaveChanges();
                await MessageBoxManager.GetMessageBoxStandard("Сообщение", "изменён", ButtonEnum.Ok).ShowAsync();
            }
            MainWindowViewModel.Instance.CurrentPage = new ListProductView();
        }
    }
}
