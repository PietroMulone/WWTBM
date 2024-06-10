using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using WPFapp.WPFLibrary.Commands;
using WWTBM.DataStructure;

namespace WWTBM.Screens.ViewModels
{
    public partial class GameViewModel : BaseModel
    {
        public ObservableCollection<AllDomanda> QuestionsTXT;
        public ObservableCollection<AllDomanda> QuestionsFOTO;
        public ObservableCollection<AllDomanda> QuestionsAUDIO;

        public delegate void AudioRefreshEvent(object sender, EventArgs e);
        public event AudioRefreshEvent AudioRefresh;


        private string _QuestionImage;
        private string _SongQuestionFileName;
        private string _Debug_text;
        private ObservableCollection<string> _RedVisible;
        private ObservableCollection<string> _Domanda;
        private ObservableCollection<string> _DomandaVisibili;
        public AllDomanda newDomanda;
        private string _ImageSource;
        private string _ImageVisible;
        private string _SoundVisible;
        private string _PlayVisible;
        private string _PauseVisible;

        private bool _isAudioOn;
        private bool _isImageOn;
        private bool _isTextOn;
        private byte _GameStatus { get; set; }

        public string QImagePath = "pack://application:,,,/WWTBM;component/images/Questions/";
        public byte Choice { get; set; }

        public MediaPlayer player;


        public GameViewModel() 
        {
            QuestionsTXT = GetQuestions(0);
            QuestionsAUDIO = GetQuestions(1);
            QuestionsFOTO = GetQuestions(2);

            player = new MediaPlayer();

            double volume = GetVolume();
            player.Volume = volume;


            _SongQuestionFileName = "";
            _ImageSource = "../Data/IMAGEQuestions/601.png";
            _ImageVisible = "Hidden";
            _SoundVisible = "Hidden";
            _PlayVisible = "Hidden";
            _PauseVisible = "Hidden";
            _Debug_text = "Debugging \n Status: " + GameStatus;
            _QuestionImage = QImagePath + "questions.png";
            Choice = 0;
            _RedVisible =new ObservableCollection<string> { "Hidden", "Hidden", "Hidden", "Hidden" };
            _Domanda = new ObservableCollection<string> { "", "r1", "r2", "r3", "r4", "1" };
            GameStatus = 0;                                                               
            _DomandaVisibili = new ObservableCollection<string> { "Hidden", "Hidden", "Hidden", "Hidden", "Hidden" };
            
        }

        public ObservableCollection<string> DomandaVisibili { get { return _DomandaVisibili; } set { _DomandaVisibili = value; OnPropertyChanged(nameof(DomandaVisibili)); } }
        public ObservableCollection<string> Domanda { get { return _Domanda; } set { _Domanda = value; OnPropertyChanged(nameof(Domanda)); } }
        public ObservableCollection<string> RedVisible { get { return _RedVisible; } set { _RedVisible = value; OnPropertyChanged(nameof(RedVisible)); } }


        public bool IsAudioOn
        {
            get { return _isAudioOn; }
            set
            {
                if (_isAudioOn != value)
                {
                    _isAudioOn = value;
                    OnPropertyChanged(nameof(IsAudioOn));
                }
            }
        }

        public bool IsImageOn
        {
            get { return _isImageOn; }
            set
            {
                if (_isImageOn != value)
                {
                    _isImageOn = value;
                    OnPropertyChanged(nameof(IsImageOn));
                }
            }
        }

        public bool IsTextOn
        {
            get { return _isTextOn; }
            set
            {
                if (_isTextOn != value)
                {
                    _isTextOn = value;
                    OnPropertyChanged(nameof(IsTextOn));
                }
            }
        }
        public string PauseVisible { get { return _PauseVisible; } set { _PauseVisible = value; OnPropertyChanged(nameof(PauseVisible)); } }
        public string PlayVisible { get { return _PlayVisible; } set { _PlayVisible = value;  OnPropertyChanged(nameof(PlayVisible)); } }
        public string SongQuestionFileName { get { return _SongQuestionFileName; } set { _SongQuestionFileName = value; OnPropertyChanged(nameof(SongQuestionFileName)); } }
        public string ImageSource { get { return _ImageSource; } set { _ImageSource = value; OnPropertyChanged(nameof(ImageSource)); } }
        public string ImageVisible { get { return _ImageVisible; } set { _ImageVisible = value; OnPropertyChanged(nameof(ImageVisible)); } }
        public string SoundVisible { get { return _SoundVisible; } set { _SoundVisible = value; OnPropertyChanged(nameof(SoundVisible)); } }
        public byte GameStatus { get { return _GameStatus; } set { _GameStatus = value; OnPropertyChanged(nameof(GameStatus));
                Debug_text = "Debugging \n Status: " + value;

                
                 switch (value)
                {
                    case 0:
                        this.newDomanda = new AllDomanda();
                        RedVisible = new ObservableCollection<string> { "Hidden", "Hidden", "Hidden", "Hidden" };
                        DomandaVisibili = new ObservableCollection<string> { "Hidden", "Hidden", "Hidden", "Hidden", "Hidden" };
                        QuestionImage = QImagePath + "questions.png";
                        player.Stop();


                        break;
                    case 1:
                        //
                        try { newDomanda = GetDomanda(QuestionsTXT, QuestionsAUDIO, QuestionsFOTO); }
                        catch { MessageBox.Show("Le Domande sono Finite"); GameStatus = 0; break; }

                        Domanda[0] = newDomanda.Titolo;
                        for (int i = 1; i < 5; i++)
                            Domanda[i] = newDomanda.Risposte[i - 1];
                        Domanda[5] = Convert.ToString(newDomanda.corretta);
                        
                        Choice = 0;

                        //

                        _DomandaVisibili[0]=  "Visible";
                        if (isWhat() == 2)
                        {
                            ImageSource = Directory.GetCurrentDirectory() + "\\Data\\IMAGEQuestions\\" + Domanda[0].Split(')')[0] + ".png";
                            ImageVisible = "Visible";
                            
                        }
                        else if (isWhat() == 1)
                        {
                            SoundVisible = "Visible";
                            PlayVisible = "Visible";
                            SongQuestionFileName = Domanda[0].Split(')')[0] + ".mp3";
                            AudioRefresh?.Invoke(this, EventArgs.Empty);
                        }
                        break;
                    case 2:
                        ComparsaRisposte();
                        break;
                    case 3:
                          if (Choice == Convert.ToByte(Domanda[5]))
                        {
                            QuestionImage = QImagePath + "v" + Choice + ".png";                         //Animazione risposta Giusta  + suono  TODO
                            player.Stop();
                            player.Open(new Uri("Audio/correct.mp3", UriKind.RelativeOrAbsolute));
                            player.Play();
                        }
                        else
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                if (i == Choice -1)
                                {
                                    QuestionImage = QImagePath + "questions.png";
                                    RedVisible[i] = "Visible";
                                    QuestionImage = QImagePath + "v" + Domanda[5] + ".png";              //Animazione risposta sbagliata + suono  TODO
                                    player.Stop();
                                    player.Open(new Uri("Audio/lose.mp3", UriKind.RelativeOrAbsolute));
                                    player.Play();
                                }
                            }
                        }
                        ImageVisible = "Hidden";
                        SoundVisible = "Hidden";
                        PlayVisible = "Hidden";
                        QuestionsTXT.Remove(this.newDomanda);
                        QuestionsAUDIO.Remove(this.newDomanda);
                        QuestionsFOTO.Remove(this.newDomanda);
                        AddBlackList(this.newDomanda.Titolo);
                        break;
                }

            } }


        internal double GetVolume()
        {
            double vol = 1;
            string percorso = "Data/Settings.txt";
            using (StreamReader sr = new StreamReader(percorso))
            {
                string riga;
                int c = 0;
                while ((riga = sr.ReadLine()) != null)
                {
                    if (c==6)
                        vol = Convert.ToDouble(riga)/100;
                    c++;
                }
            }
            return vol;
        }


        internal int isWhat()   // 0 = txt, 1 = audio, 2 = foto
        {
            int DomandaNumero = Convert.ToInt32(this.newDomanda.Titolo.Split(')')[0]);
            if (DomandaNumero < 300)
                return 0;
            if (DomandaNumero >= 300 && DomandaNumero < 600)
                return 1;
            if (DomandaNumero >= 600)
                return 2;

            return 0;
        }
        internal void AddBlackList(string Titolo)
        {
            string percorso = "Data/BlackList.txt";
            string numero = "";
            foreach(char c in Titolo)
            {
                if (char.IsDigit(c))
                    numero += c;
            }
            using (StreamWriter writer = new StreamWriter(percorso, true))                  //→ append
            {
                writer.Write("," + numero);
            }

        }

        internal AllDomanda GetDomanda(ObservableCollection<AllDomanda>  QuestionsTXT, ObservableCollection<AllDomanda> Questionsmp3, ObservableCollection<AllDomanda> QuestionsFoto)
        {
            ObservableCollection<AllDomanda> Abbeveratoio = new ObservableCollection<AllDomanda>();

            if (!IsAudioOn)
                foreach (AllDomanda Domanda in Questionsmp3)
                    Abbeveratoio.Add(Domanda);

            if (!IsImageOn)
                foreach (AllDomanda Domanda in QuestionsFoto)
                    Abbeveratoio.Add(Domanda);
            if (!IsTextOn)
                foreach (AllDomanda Domanda in QuestionsTXT)
                    Abbeveratoio.Add(Domanda);

            Random random = new Random();
            int randomNumber = random.Next(0, Abbeveratoio.Count());
            AllDomanda newDomanda = Abbeveratoio[randomNumber];
            return newDomanda;
        }

        private async Task ComparsaRisposte()
        {
            int tempo;
            for (int i = 1; i < 5; i++)
            {
                _DomandaVisibili[i] = "Visible";
                tempo = _Domanda[i+2].Split(' ').Length*200;        //metti a 200
                if (tempo < 1000)
                    tempo = 1200;                       
                await Task.Delay(tempo);
            }
            
        }
        public string QuestionImage { get { return _QuestionImage; } set { _QuestionImage = value; OnPropertyChanged(nameof(QuestionImage)); } }
        public string Debug_text { get { return _Debug_text; } set { _Debug_text = value; OnPropertyChanged(nameof(Debug_text));  } }



        internal ObservableCollection<AllDomanda> GetQuestions(byte type)
        {
            ObservableCollection<AllDomanda> quest = new ObservableCollection<AllDomanda>();
            switch (type)
                {
                case 0:
                    string percorsotxt = "Data/TXTQuestions/Questions.txt";
                    GetFileQuestions(percorsotxt, quest);
                    break;
                case 1:
                    string percorsomp3 = "Data/AUDIOQuestions/Questions.txt";
                    GetFileQuestions(percorsomp3, quest);
                    break;
                case 2:
                    string percorsofoto = "Data/IMAGEQuestions/Questions.txt";
                    GetFileQuestions(percorsofoto, quest);
                    break;
            }

            RimuoviBlackList(quest);
            return quest;
        }
        internal void GetFileQuestions(string percorso, ObservableCollection<AllDomanda> quest)
        {
            using (StreamReader sr = new StreamReader(percorso))
            {
                AllDomanda singola = new AllDomanda();
                string riga;
                byte n = 0;
                while ((riga = sr.ReadLine()) != null)
                {
                    switch (n)
                    {
                        case 0:
                            singola.Titolo = riga;
                            n++;
                            break;
                        case 1:
                            singola.Risposte[0] = riga;
                            n++;
                            break;
                        case 2:
                            singola.Risposte[1] = riga;
                            n++;
                            break;
                        case 3:
                            singola.Risposte[2] = riga;
                            n++;
                            break;
                        case 4:
                            singola.Risposte[3] = riga;
                            n++;
                            break;
                        case 5:
                            singola.corretta = Convert.ToByte(riga);
                            n = 0;
                            quest.Add(singola);     //<<--
                            singola = new AllDomanda();
                            break;
                    }
                }
            }
        }

        internal void RimuoviBlackList(ObservableCollection<AllDomanda> quest)
        {
            string percorso = "Data/BlackList.txt";
            string file = File.ReadAllText(percorso);
            string[] lista = file.Split(',');
            List<AllDomanda> itemsToRemove = new List<AllDomanda>();

            foreach (string numero in lista)
            {
                foreach (AllDomanda Rdomanda in quest.ToList()) // Iterate over a copy of the collection
                {
                    if (Rdomanda.Titolo.StartsWith(numero) && numero != "")
                    {
                        itemsToRemove.Add(Rdomanda);
                    }
                }
            }
            foreach (var itemToRemove in itemsToRemove)
            {
                quest.Remove(itemToRemove);
            }
        }



    }
}
