using AppBlocking2.Models;
using AppBlocking2.Pages;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppBlocking2.ViewModels
{
    public class BlockingViewModel : ViewModelBase
    {
        private NavegacaoTela _navegacaoTela;
        private int _contadorTela;
        private readonly BlockingCollection<string> _queue = new BlockingCollection<string>(new ConcurrentQueue<string>());

        private ObservableCollection<string> _listaTela;
        public ObservableCollection<string> ListaTela
        {
            get { return _listaTela; }
            set
            {
                _listaTela = value;
                OnPropertyChanged();
            }
        }

        private ICommand _adicionarNumeroCommand;
        public ICommand AdicionarNumeroCommand
        {
            get { return _adicionarNumeroCommand; }
            set
            {
                _adicionarNumeroCommand = value;
                OnPropertyChanged();
            }
        }

        private ICommand _interromperNavegarCommand;
        public ICommand InterromperNavegarCommand
        {
            get { return _interromperNavegarCommand; }
            set
            {
                _interromperNavegarCommand = value;
                OnPropertyChanged();
            }
        }

        private ICommand _voltarTelaCommand;
        public ICommand VoltarTelaCommand
        {
            get { return _voltarTelaCommand; }
            set
            {
                _voltarTelaCommand = value;
                OnPropertyChanged();
            }
        }

        private string _descricaoQueue;

        public string DescricaoQueue
        {
            get { return _descricaoQueue; }
            set
            {
                _descricaoQueue = value;
                OnPropertyChanged();
            }
        }


        public BlockingViewModel()
        {
            ListaTela = new ObservableCollection<string>();

            AdicionarNumeroCommand = new Command(AdicionarNumero);
            InterromperNavegarCommand = new Command(InterromperNavegar);
            VoltarTelaCommand = new Command(VoltarTela);

            var taskmonitor = Task.Run(() => Monitor());
        }

        public async Task Monitor()
        {
            try
            {
                while (true)
                {
                    var nextJob = _queue.Take();
                    await ProcessarFila(nextJob);
                }
            }
            catch (InvalidOperationException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if(_navegacaoTela == NavegacaoTela.ProximaTela)
                        await Navigation.PushAsync(new Page2());
                    else
                        await Navigation.PopAsync();
                });
            }
        }

        private async Task ProcessarFila(string entrada)
        {
            DescricaoQueue = "Processando " + entrada + "...";
            await Task.Delay(TimeSpan.FromSeconds(1));

            ListaTela.Add(entrada);

            DescricaoQueue = "Concluido";
        }

        private void AdicionarNumero()
        {
            _contadorTela++;
            var texto = "Label " + _contadorTela;

            _queue.Add(texto);
        }

        private void InterromperNavegar()
        {
            _navegacaoTela = NavegacaoTela.ProximaTela;
            _queue.CompleteAdding();
        }

        private void VoltarTela()
        {
            _navegacaoTela = NavegacaoTela.TelaAnterior;
            _queue.CompleteAdding();
        }
    }
}
