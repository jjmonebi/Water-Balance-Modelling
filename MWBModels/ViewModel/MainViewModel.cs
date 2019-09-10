using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MWBModels.Model;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Windows;

namespace MWBModels.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public RelayCommand OpenCommand { get; set; }
        public RelayCommand RunCommand { get; set; }

        private ObservableCollection _DefaultViewModel = new ObservableCollection();
        public ObservableCollection DefaultViewModel
        {
            get
            {
                return _DefaultViewModel;
            }
            set
            {
                Set(ref _DefaultViewModel, value);
            }
        }
        
        public MainViewModel()
        {
            //_dataService = dataService;
            OpenCommand = new RelayCommand(OpenFile);
            RunCommand = new RelayCommand(RunModels);
        }

        private void RunModels()
        {
            if(DefaultViewModel.FileName == "Select File")
            {
                MessageBox.Show("Please select a CSV file");
                return;
            }
            else if(!DefaultViewModel.MTM && !DefaultViewModel.GR2M && !DefaultViewModel.GR5M)
            {
                MessageBox.Show("Please select a model");
                return;
            }
            else
            {
                try
                {
                    var result = ProcessResult.GenerateResult(DefaultViewModel, DefaultViewModel.FileName);
                    ResultWindow win = new ResultWindow(result);
                    win.Show();
                    GraphWindow graph = new GraphWindow(result, DefaultViewModel);
                    graph.Show();
                }
                catch
                {
                    MessageBox.Show("Invalid CSV file");
                }
                
            }
        }

        private void OpenFile()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "CSV Files (*.csv)|*.csv";
            if (fileDialog.ShowDialog() == true)
            {
                DefaultViewModel.FileName = fileDialog.FileName;
            }
        }
    }
}