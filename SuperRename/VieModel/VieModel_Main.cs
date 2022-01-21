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

namespace SuperRename.VieModel
{
    public class VieModel_Main : ViewModelBase
    {


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



        public void ApplyChanges()
        {
            // 修改后缀名
            if (ChangeExt && !string.IsNullOrEmpty(Ext) && DataList?.Count > 0)
            {
                for (int i = 0; i < DataList.Count; i++)
                {
                    DataList[i].Target = Path.GetDirectoryName(DataList[i].Target) + Path.GetFileNameWithoutExtension(DataList[i].Target) + "." + Ext;
                }
            }
        }



        public VieModel_Main()
        {
            LoadSample();
        }

        private void LoadSample()
        {
            DataList = new ObservableCollection<FileData>();
            for (int i = 0; i < 10; i++)
            {
                DataList.Add(new FileData(true, $"d:\\file{i}.zip", "d:\\new_file" + (i % 3 == 0 ? "/" : "") + $"{i}.zip"));
            }
        }
    }
}
