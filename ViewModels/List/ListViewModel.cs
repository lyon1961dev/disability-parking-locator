﻿using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

using dpark.Models;
using dpark.Models.Data;
using dpark.ViewModels.Base;

using Xamarin.Forms;
using System.Threading.Tasks;

namespace dpark.ViewModels.List
{
    public class ListViewModel : BaseViewModel
    {
        List<tmpSpaceData> _tmpSpaceData;
        ObservableCollection<tmpSpaceData> SortSpaceData;
        public ObservableCollection<tmpSpaceData> SortedSpaceData
        {
            get { return SortSpaceData; }
            set { SortSpaceData = value; }
        }

        public ListViewModel()
        {
            _tmpSpaceData = new List<tmpSpaceData>();
            SortedSpaceData = new ObservableCollection<tmpSpaceData>();
        }

        Command _LoadSpaces;
        public Command LoadSpaces
        {
            get
            {
                return _LoadSpaces ??
                    (_LoadSpaces = new Command(async () =>
                    await ExecuteLoadSpaceCommand()));     
            }
        }

        public async Task ExecuteLoadSpaceCommand()
        {
            IsBusy = true;

            var items = new List<tmpSpaceData>();
            foreach (var item in AppData.Spaces.tmpSpaceCollection)
            {
                items.Add(item);
            }

            _tmpSpaceData.Clear();
            _tmpSpaceData.AddRange(Sort(items));

            SortedItems();
          
            IsBusy = false;
        }

        void SortedItems()
        {
            SortedSpaceData.Clear();
            SortedSpaceData.AddRange(_tmpSpaceData);
            Debug.WriteLine(SortedSpaceData.Count);
        }
        static IEnumerable<tmpSpaceData> Sort(IEnumerable<tmpSpaceData> temp)
        {
            return temp.OrderByDescending(x => x.ID).ThenByDescending(x => x.DistanceFrom);
        }
    }
}
