using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using ValoExplorer.Models;
using ValoExplorer.Services;

namespace ValoExplorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BackgroundWorker worker = new BackgroundWorker();
        private readonly RiotClientService service = new RiotClientService();
        public MainWindow()
        {
            InitializeComponent();
            treeView.Opacity = 0;
            valoText.Opacity = 100;
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
            valoText.Content = "WAITING VALORANT";
        }
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!service.RiotServiceIsRunning())
            {
                Thread.Sleep(100);
            }
        }
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            valoText.Dispatcher.Invoke(() =>
            {
                valoText.Opacity = 0;
                treeView.Opacity = 100;
                foreach (var list in service.StartListing())
                {
                    var treeItem = new TreeViewItem();
                    treeItem.Header = list.Name;
                    treeItem.Tag = "API";
                    foreach (var reqTypes in list.Body)
                    {
                        var altTree = new TreeViewItem { Tag = reqTypes.ReqName, Header = "" };
                        if (reqTypes.ReqBody.OperationId != null)
                        {
                            altTree.Items.Add(new TreeViewItem { Tag = "OPERATIONID", Header = reqTypes.ReqBody.OperationId,Foreground = Brushes.White });
                        }
                        if (reqTypes.ReqBody.Description != null)
                        {
                            altTree.Items.Add(new TreeViewItem { Tag = "DESCRIPTION", Header = reqTypes.ReqBody.Description, Foreground = Brushes.White });
                        }
                        if (reqTypes.ReqBody.Summary != null)
                        {
                            altTree.Items.Add(new TreeViewItem { Tag = "SUMMARY", Header = reqTypes.ReqBody.Summary, Foreground = Brushes.White });
                        }
                        if (reqTypes.ReqBody.Parameters.Any())
                        {
                            var secondAltTree = new TreeViewItem { Tag = "PARAMETERS", Foreground = Brushes.Tomato };
                            altTree.Items.Add(secondAltTree);
                            foreach (var param in reqTypes.ReqBody.Parameters)
                            {
                                if (param.In != null)
                                {
                                    secondAltTree.Items.Add(new TreeViewItem { Tag = "IN", Header = param.In });
                                }
                                if (param.Name != null)
                                {
                                    secondAltTree.Items.Add(new TreeViewItem { Tag = "NAME", Header = param.Name });
                                }
                                if (param.ParameterRequired != null)
                                {
                                    secondAltTree.Items.Add(new TreeViewItem { Tag = "REQUIRED", Header = param.ParameterRequired });
                                }
                                if (param.Type != null)
                                {
                                    secondAltTree.Items.Add(new TreeViewItem { Tag = "TYPE", Header = param.Type });
                                }
                            }
                        }
                        treeItem.Items.Add(altTree);
                    }
                    treeView.Items.Add(treeItem);
                }
            });
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
