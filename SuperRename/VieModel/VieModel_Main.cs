using GalaSoft.MvvmLight;
using SuperRename.Core.pojo;
using SuperRename.Core.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SuperRename.VieModel
{
    public class VieModel_Main : ViewModelBase
    {

        public void DataListCountChange()
        {
            ShowDragHint = DataList?.Count <= 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private ObservableCollection<FileData> _DataList;
        public ObservableCollection<FileData> DataList
        {
            get { return _DataList; }
            set
            {
                _DataList = value;
                RaisePropertyChanged();
            }
        }

        private string _Ext;
        public string Ext
        {
            get { return _Ext; }
            set
            {
                _Ext = value;
                RaisePropertyChanged();
            }
        }
        private bool _ChangeExt;
        public bool ChangeExt
        {
            get { return _ChangeExt; }
            set
            {
                _ChangeExt = value;
                RaisePropertyChanged();
            }
        }


        private bool _CanRun = false;
        public bool CanRun
        {
            get { return _CanRun; }
            set
            {
                _CanRun = value;
                RaisePropertyChanged();
            }
        }

        private bool _ExpandTogglePanel;
        public bool ExpandTogglePanel
        {
            get { return _ExpandTogglePanel; }
            set
            {
                _ExpandTogglePanel = value;
                RaisePropertyChanged();
            }
        }

        private Visibility _ShowDragHint;
        public Visibility ShowDragHint
        {
            get { return _ShowDragHint; }
            set
            {
                _ShowDragHint = value;
                RaisePropertyChanged();
            }
        }



        public void ApplyChanges()
        {
            // 修改后缀名
            if (ChangeExt && !string.IsNullOrEmpty(Ext) && DataList?.Count > 0)
            {
                for (int i = 0; i < DataList.Count; i++)
                {
                    DataList[i].Target = Path.Combine(Path.GetDirectoryName(DataList[i].Target),
                        Path.GetFileNameWithoutExtension(DataList[i].Target) + "." + Ext);
                }
            }
        }



        public VieModel_Main()
        {
            DataList = new ObservableCollection<FileData>();
            DataListCountChange();
            //LoadSample();
        }

        private void LoadSample()
        {
            DataList = new ObservableCollection<FileData>();
            for (int i = 0; i < 10; i++)
            {
                DataList.Add(new FileData(true, $"d:\\file{i}.zip", "d:\\new_file" + (i % 3 == 0 ? "/" : "") + $"{i}.zip"));
            }
            DataListCountChange();
        }
    }
}
