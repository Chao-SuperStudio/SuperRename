using ChaoControls.Style;
using SuperRename.Core.pojo;
using SuperRename.Core.Utils;
using SuperRename.VieModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SuperRename
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : BaseWindow
    {
        VieModel_Main vieModel;
        public MainWindow()
        {
            InitializeComponent();
            vieModel = new VieModel_Main();
            this.DataContext = vieModel;
        }

        private void DataGrid_Drop(object sender, DragEventArgs e)
        {
            string[] dragdropFiles = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (dragdropFiles != null && dragdropFiles.Length > 0)
            {
                for (int i = 0; i < dragdropFiles.Length; i++)
                {
                    vieModel.DataList.Add(new FileData(true, dragdropFiles[i], dragdropFiles[i]));
                }
                vieModel.DataListCountChange();
            }

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SetEnable();
        }

        private void SetEnable(bool value = true)
        {
            for (int i = 0; i < vieModel?.DataList.Count; i++)
            {
                vieModel.DataList[i].Enable = value;
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            SetEnable(false);
        }

        private void ChangeExt(object sender, RoutedEventArgs e)
        {
            vieModel.ApplyChanges();
        }

        private void BeginChangeName(object sender, RoutedEventArgs e)
        {
            if (!CheckNameProper())
            {
                ChaoControls.Style.MessageCard.Show("文件名非法，请修改后再执行");
                return;
            }

            int breakCount = 0;
            List<FileData> success = new List<FileData>();

            if (vieModel?.DataList.Count > 0)
            {
                // 执行修改
                for (int i = 0; i < vieModel.DataList.Count; i++)
                {
                    FileData data = vieModel.DataList[i];
                    string source = data.Source;
                    string target = data.Target;
                    if (source.Equals(target)) continue;
                    try
                    {
                        File.Move(vieModel.DataList[i].Source, vieModel.DataList[i].Target);
                        success.Add(data);
                    }
                    catch (Exception ex)
                    {
                        ChaoControls.Style.MessageCard.Show(ex.Message);
                        breakCount++;
                        if (breakCount >= 10) break;
                    }
                }
                ChaoControls.Style.MessageCard.Show($"修改文件名：{success.Count}/{vieModel.DataList.Count}", MessageCard.MessageCardType.Succes);
            }
        }

        private bool CheckNameProper()
        {
            List<int> wrongList = new List<int>();


            for (int i = 0; i < vieModel?.DataList.Count; i++)
            {
                FileData data = vieModel.DataList[i];
                if (data.Enable)
                {
                    string target = data.Target;
                    if (string.IsNullOrEmpty(target) || !FileUtils.IsProperPath(target))
                    {
                        wrongList.Add(i);
                    }
                }
                setDataGridColor(i, Brushes.White);
            }

            for (int i = 0; i < wrongList.Count; i++)
            {
                setDataGridColor(wrongList[i], Brushes.Red);
            }


            return wrongList.Count <= 0;
        }

        private void setDataGridColor(int rowIndex, SolidColorBrush brush)
        {
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator
                                               .ContainerFromIndex(rowIndex);
            if (row == null) return;
            var cell = dataGrid.Columns[3];
            var cp = (ContentPresenter)cell.GetCellContent(row);
            TextBox textBox = (TextBox)cp.ContentTemplate.FindName("tb", cp);
            textBox.Foreground = brush;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            vieModel.DataList.Clear();
            vieModel.DataListCountChange();
        }
    }
}
