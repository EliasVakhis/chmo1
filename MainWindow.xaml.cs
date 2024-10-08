using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace chmo
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        workshopEntities1 db = new workshopEntities1();

        public double firstPrice, secondPrice, thirdPrice, firstProfit, secondProfit, thirdProfit, profit, fullProfit, loss = 0;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            statisticGrid.ItemsSource = db.otchet.ToList();
        }

        public int firstNorm, secondNorm, thirdNorm, firstFact, secondFact, thirdFact;

        public MainWindow()
        {
            InitializeComponent();
        }
        public int raschetMonth(int fact)
        {
            int month = fact * 8 * 30;
            return month;
        }


        private void reloadBtn_Click(object sender, RoutedEventArgs e)
        {
            firstFactTxt.Text = string.Empty;
            secondFactTxt.Text = string.Empty;
            thirdFactTxt.Text = string.Empty;
            firstNormTxt.Text = string.Empty;
            secondNormTxt.Text = string.Empty;
            thirdNormTxt.Text = string.Empty;
        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            var worker = db.worker.FirstOrDefault(x=>x.id== 1);
            firstPrice = (double)worker.price;
            worker = db.worker.FirstOrDefault(x => x.id == 2);
            secondPrice = (double)worker.price;
            worker = db.worker.FirstOrDefault(x => x.id == 3);
            thirdPrice = (double)worker.price;


            if (firstNormTxt.Text == "" || firstFactTxt.Text == "")
            {
                MessageBox.Show("Введите данные для первого цеха.");
            }
            else if (secondNormTxt.Text == "" || secondFactTxt.Text == "")
            {
                MessageBox.Show("Введите данные для второго цеха.");
            }
            else if (thirdNormTxt.Text == "" || thirdFactTxt.Text == "")
            {
                MessageBox.Show("Введите данные для третьего цеха.");
            }
            else if (yearTxt.Text == "" || monthBox.Text =="")
            {
                MessageBox.Show("Введите месяц и год.");
            }
            else
            {
                firstNorm = Convert.ToInt32(firstNormTxt.Text);
                firstFact = raschetMonth(Convert.ToInt32(firstFactTxt.Text));
                secondNorm = Convert.ToInt32(secondNormTxt.Text);
                secondFact = raschetMonth(Convert.ToInt32(secondFactTxt.Text));
                thirdNorm = Convert.ToInt32(thirdNormTxt.Text);
                thirdFact = raschetMonth(Convert.ToInt32(thirdFactTxt.Text));

                firstProfit = firstFact * firstPrice;
                secondProfit = secondFact * secondPrice;
                thirdProfit = thirdFact * thirdPrice;

                if (firstFact >= firstNorm)
                {
                    if (secondFact >= secondNorm)
                    {
                        if (thirdFact >= thirdNorm)
                        {
                            profit = firstProfit + secondProfit + thirdProfit;
                            MessageBox.Show("Profit: " + profit);
                        }
                        else
                        {
                            
                            MessageBox.Show("3 neProfit: " + profit);
                        }
                    }
                    else
                    {
                        if (thirdFact >= thirdNorm)
                        {
                            profit = (firstProfit + thirdProfit) - ((firstProfit + thirdProfit) * 0.2);
                            MessageBox.Show("2 neProfit: " + profit);
                        }
                        else
                        {
                            profit = (firstProfit) - (firstProfit * 0.2);
                            MessageBox.Show("2 and 3 neProfit: " + profit);
                        }
                    }
                }
                else
                {
                    if (secondFact >= secondNorm)
                    {
                        if (thirdFact >= thirdNorm)
                        {
                            profit = (secondProfit + thirdProfit) - ((secondProfit + thirdProfit) * 0.2);
                            MessageBox.Show("1 neProfit: " + profit);
                        }
                        else
                        {
                            profit = (secondProfit) - (secondProfit * 0.2);
                            MessageBox.Show("1 and 3 neProfit: " + profit);
                        }
                    }
                    else
                    {
                        if (thirdFact >= thirdNorm)
                        {
                            profit = (thirdProfit) - (thirdProfit * 0.2);
                            MessageBox.Show("1 and 2 neProfit: " + profit);
                        }
                        else
                        { 
                            profit = 0;
                            MessageBox.Show("1 and 2 and 3 neProfit: " + profit);
                        }
                    }
                }
                fullProfit = firstProfit + secondProfit + thirdProfit;
                loss = fullProfit - profit;
                otchet otchet = new otchet()
                {
                    profit = profit,
                    Year = yearTxt.Text,
                    Month = monthBox.Text,
                    loss = loss
                };
                db.otchet.Add(otchet);
                db.SaveChanges();
            }
        }
    }
}
