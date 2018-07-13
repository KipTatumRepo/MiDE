using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using MiDEWPF.Models;
using MiDEWPF.ViewModel;

namespace MiDEWPF.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public ObservableCollection<MenuItemViewModel> MenuItems { get; set; }


        public Home()
        {

            InitializeComponent();

            MiDEDataSet ds = ((MiDEDataSet)(FindResource("mideDataSet")));

            MiDEDataSetTableAdapters.MiDEBuildingsTableAdapter adapter = new MiDEDataSetTableAdapters.MiDEBuildingsTableAdapter();
            MiDEDataSetTableAdapters.MiDEPopulationTableAdapter padapter = new MiDEDataSetTableAdapters.MiDEPopulationTableAdapter();
            MiDEDataSetTableAdapters.MiDESValuesTableAdapter sadapter = new MiDEDataSetTableAdapters.MiDESValuesTableAdapter();
            MiDEDataSetTableAdapters.MiDEStrategyGroupsTableAdapter stadapter = new MiDEDataSetTableAdapters.MiDEStrategyGroupsTableAdapter();
            MiDEDataSetTableAdapters.MiDEEValuesTableAdapter eadapter = new MiDEDataSetTableAdapters.MiDEEValuesTableAdapter();

            adapter.Fill(ds.MiDEBuildings);
            padapter.Fill(ds.MiDEPopulation);
            sadapter.Fill(ds.MiDESValues);
            stadapter.Fill(ds.MiDEStrategyGroups);
            eadapter.Fill(ds.MiDEEValues);

            #region Building Menu Creation
            MenuItems = new ObservableCollection<MenuItemViewModel>
            {
               
                new MenuItemViewModel { Header = "Select a Building",
                    MenuItems = new ObservableCollection<MenuItemViewModel>
                        {
                            new MenuItemViewModel { Header = "Building 4",
                            MenuItems = new ObservableCollection<MenuItemViewModel>
                                {
                                    new MenuItemViewModel { Header = "< 500",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "500 - 999",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "1000 - 1249",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                        }
                                    },
                                    new MenuItemViewModel { Header = "1250 - 1499",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "> 1500",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                        }
                                    }
                                }
                            },
                            new MenuItemViewModel { Header = "Building 9",
                            MenuItems = new ObservableCollection<MenuItemViewModel>
                                {
                                    new MenuItemViewModel { Header = "< 500",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "500 - 999",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "1000 - 1249",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "1250 - 1499",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "> 1500",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    }
                                }
                            },
                            new MenuItemViewModel { Header = "Building 16",
                            MenuItems = new ObservableCollection<MenuItemViewModel>
                                {
                                    new MenuItemViewModel { Header = "< 500",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "500 - 999",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "1000 - 1249",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "1250 - 1499",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "> 1500",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    }
                                }
                            },
                            new MenuItemViewModel { Header = "Building 25",
                            MenuItems = new ObservableCollection<MenuItemViewModel>
                                {
                                    new MenuItemViewModel { Header = "< 500",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "500 - 999",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "1000 - 1249",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "1250 - 1499",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "> 1500",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    }
                                }
                            },
                            new MenuItemViewModel { Header = "Building 26",
                            MenuItems = new ObservableCollection<MenuItemViewModel>
                                {
                                    new MenuItemViewModel { Header = "< 500",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "500 - 999",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "1000 - 1249",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "1250 - 1499",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "> 1500",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    }
                                }
                            },
                            new MenuItemViewModel { Header = "Building 31",
                            MenuItems = new ObservableCollection<MenuItemViewModel>
                                {
                                    new MenuItemViewModel { Header = "< 500",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "500 - 999",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "1000 - 1249",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "1250 - 1499",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "> 1500",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    }
                                }
                            },
                            new MenuItemViewModel { Header = "Building 36",
                            MenuItems = new ObservableCollection<MenuItemViewModel>
                                {
                                    new MenuItemViewModel { Header = "< 500",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "500 - 999",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "1000 - 1249",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "1250 - 1499",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "> 1500",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    }
                                }
                            },
                            new MenuItemViewModel { Header = "Building 41",
                            MenuItems = new ObservableCollection<MenuItemViewModel>
                                {
                                    new MenuItemViewModel { Header = "< 500",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "500 - 999",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "1000 - 1249",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "1250 - 1499",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "> 1500",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    }
                                }
                            },
                            new MenuItemViewModel { Header = "Building 43",
                            MenuItems = new ObservableCollection<MenuItemViewModel>
                                {
                                    new MenuItemViewModel { Header = "< 500",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "500 - 999",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "1000 - 1249",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "1250 - 1499",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "> 1500",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    }
                                }
                            },
                            new MenuItemViewModel { Header = "Building 50",
                            MenuItems = new ObservableCollection<MenuItemViewModel>
                                {
                                    new MenuItemViewModel { Header = "< 500",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "500 - 999",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "1000 - 1249",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "1250 - 1499",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "> 1500",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    }
                                }
                            },
                            new MenuItemViewModel { Header = "Building 83",
                            MenuItems = new ObservableCollection<MenuItemViewModel>
                                {
                                    new MenuItemViewModel { Header = "< 500",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "500 - 999",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "1000 - 1249",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "1250 - 1499",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "> 1500",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    }
                                }
                            },
                            new MenuItemViewModel { Header = "Building 86",
                            MenuItems = new ObservableCollection<MenuItemViewModel>
                                {
                                    new MenuItemViewModel { Header = "< 500",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "500 - 999",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "1000 - 1249",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "1250 - 1499",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "> 1500",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    }
                                }
                            },
                            new MenuItemViewModel { Header = "Building 92",
                            MenuItems = new ObservableCollection<MenuItemViewModel>
                                {
                                    new MenuItemViewModel { Header = "< 500",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "500 - 999",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "1000 - 1249",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "1250 - 1499",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "> 1500",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    }
                                }
                            },
                            new MenuItemViewModel { Header = "Building 99",
                            MenuItems = new ObservableCollection<MenuItemViewModel>
                                {
                                    new MenuItemViewModel { Header = "< 500",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "500 - 999",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "1000 - 1249",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "1250 - 1499",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "> 1500",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    }
                                }
                            },
                            new MenuItemViewModel { Header = "Building 109",
                            MenuItems = new ObservableCollection<MenuItemViewModel>
                                {
                                    new MenuItemViewModel { Header = "< 500",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "500 - 999",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "1000 - 1249",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "1250 - 1499",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "> 1500",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    }
                                }
                            },
                            new MenuItemViewModel { Header = "Building 112",
                            MenuItems = new ObservableCollection<MenuItemViewModel>
                                {
                                    new MenuItemViewModel { Header = "< 500",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "500 - 999",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "1000 - 1249",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "1250 - 1499",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "> 1500",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    }
                                }
                            },
                            new MenuItemViewModel { Header = "Building 121",
                            MenuItems = new ObservableCollection<MenuItemViewModel>
                                {
                                    new MenuItemViewModel { Header = "< 500",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "500 - 999",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "1000 - 1249",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "1250 - 1499",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "> 1500",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    }
                                }
                            },
                            new MenuItemViewModel { Header = "Studio H",
                            MenuItems = new ObservableCollection<MenuItemViewModel>
                                {
                                    new MenuItemViewModel { Header = "< 500",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "500 - 999",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "1000 - 1249",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "1250 - 1499",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    },
                                    new MenuItemViewModel { Header = "> 1500",
                                        MenuItems = new ObservableCollection<MenuItemViewModel>
                                        {
                                            new MenuItemViewModel {Header = "Executives" },
                                            new MenuItemViewModel {Header = "Engineers" },
                                            new MenuItemViewModel {Header = "Sales" },
                                            new MenuItemViewModel {Header = "Marketing"},
                                            new MenuItemViewModel {Header = "Support"}
                                          }
                                    }
                                }
                            }
                    }


                }

            };
            
            buildingMenu.DataContext = this;
            
        }
        #endregion

        private void selectBuildingCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string add = selectBuildingCB.SelectedValue.ToString();
            selectionListBox.Items.Add(add);
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            string add = datePicker.SelectedDate.ToString();
            selectionListBox.Items.Add(add);
        }

        private void sFactors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string add = sFactorCB.SelectedValue.ToString();
            selectionListBox.Items.Add(add);
        }

        private void selectExclusionCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string add = strategyExclusionCB.SelectedValue.ToString();
            exclusionSelectionListBox.Items.Add(add);
        }

        private void mitigationExclusion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string add = mitigationExclusionCB.SelectedValue.ToString();
            exclusionSelectionListBox.Items.Add(add);
        }

        private void buildingMenu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(
                new Uri("Pages/MiDESelection.xaml", UriKind.Relative));
        }
    }
}
