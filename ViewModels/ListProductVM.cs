using DemoEkzZachet.Models;
using DemoEkzZachet.Views;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using System.Collections.Generic;
using System.Linq;


namespace DemoEkzZachet.ViewModels
{
    public class ListProductVM : ViewModelBase
    {

        //private List<Hotel> _hotels = new List<Hotel>();
        //public List<Hotel> Hotels { get => _hotels; set => this.RaiseAndSetIfChanged(ref _hotels, value); }

        private List<Tour> _tours = new List<Tour>();
        private List<Tour> _toursPreview = new List<Tour>();
        private List<string> _sortTypes = new List<string>()
            {
            "Без сортировки",
            "По убыванию цены",
            "По возрастанию цены"
            };
        private List<TypeEntity> _filterTypes = new List<TypeEntity>() { new TypeEntity() { Id = 0, TypeName = "Без фильрации" } };
        private int _selectSortType = 0;
        private int _selectfilterType = 0;
        private string _search = "";
        public List<Tour> ToursPreview
        {
            get => _toursPreview;
            set => this.RaiseAndSetIfChanged(ref _toursPreview, value);
        }
        public List<string> SortTypes
        {
            get => _sortTypes;
            set => this.RaiseAndSetIfChanged(ref _sortTypes, value);
        }
        public List<TypeEntity> FilterTypes
        {
            get => _filterTypes;
            set => this.RaiseAndSetIfChanged(ref _filterTypes, value);
        }
        public int SelectSortType
        {
            get => _selectSortType;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectSortType, value);
                Filter();
            }
        }
        public int SelectFilerType
        {
            get => _selectfilterType;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectfilterType, value);
                Filter();
            }
        }
        public string Search
        {
            get => _search;
            set
            {
                this.RaiseAndSetIfChanged(ref _search, value);
                Filter();
            }
        }

        void Filter()
        {
            ToursPreview = _tours; //заново получаем весь лист
            if (Search != "")
            {
                ToursPreview = ToursPreview.Where(t => t.Name.ToLower().Contains(Search.ToLower())).ToList();
            }
            if (SelectFilerType != 0)
            {
                TypeEntity selectType = FilterTypes[SelectFilerType];
                ToursPreview = ToursPreview.Where(t => t.ToursTypes.Any(it => it.Type == selectType)).ToList();
            }
            if (SelectSortType != 0)
            {
                if (SelectSortType == 2) ToursPreview = ToursPreview.OrderBy(t => t.Price).ToList();
                if (SelectSortType == 1) ToursPreview = ToursPreview.OrderByDescending(t => t.Price).ToList();
            }
        }
        public ListProductVM()
        {
            _tours = MainWindowViewModel.Context.Tours
                .Include(it => it.ToursTypes).ThenInclude(it => it.Type)
                .ToList();
            ToursPreview = _tours;

            List<TypeEntity> types = MainWindowViewModel.Context.Types.ToList();
            FilterTypes = types;
            FilterTypes.Insert(0, new TypeEntity() { Id = 0, TypeName = "Без фильрации" });

            /*Hotels = MainWindowViewModel.Context.Hotels
                //по листу связей ICollection ToursTypes к полю связи Type Type
                .Include(it => it.CountryCodeNavigation).ToList();*/
        }

        public void GoEdit(Tour tour)
        {
            MainWindowViewModel.Instance.CurrentPage = new ProductEditorView(tour);
        }

        public void Add()
        {
            MainWindowViewModel.Instance.CurrentPage = new AddTourView();
        }

    }
}
