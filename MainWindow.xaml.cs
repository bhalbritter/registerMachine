using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Registermaschiene
{
    //    Code für die Summe der Quadratzahlen
    //          MOV EBX 1
    //          MOV EAX 0
    //          MOV EBX 0
    //          MOV ECX 0 
    //          MOV EDX 0
    //          MOV CX 99
    //          :zeiger
    //          MOV EAX EBX
    //          MUL EAX
    //          ADD EDX
    //          MOV EDX EAX
    //          INC ebx
    //          LOOP zeiger
    //          LOG EDX

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int eax, ebx, ecx, edx, cx = 0;
        TextBox code;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void textChangedEventHandler(object sender, TextChangedEventArgs args)
        {
            code = (TextBox)sender;
        }

        private int findLine(String key)
        {
            String text = code.Text;
            string[] lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Trim().Equals(":" + key))
                {
                    return i + 1;
                }
            }
            MessageBox.Show("Sprungziel nicht gefunden! Breche ab.");
            return lines.Length; ;
        }

        private int getRegister(String register)
        {
            register = register.ToLower();

            switch (register)
            {
                case "eax":
                    return eax;
                case "ebx":
                    return ebx;
                case "ecx":
                    return ecx;
                case "edx":
                    return edx;
                case "cx":
                    return cx;
                default:
                    return Int32.Parse(register);
            }
        }

        private void runCode(object sender, RoutedEventArgs e)
        {
            Dictionary<int, string> map = new Dictionary<int, string>();
            String text = code.Text;
            int currlineNo = 1;
            string currline;
            string[] lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            for (int i = 0; i < lines.Length; i++)
            {
                map.Add(i + 1, lines[i]);
            }

            do
            {
                currline = map[currlineNo].Trim();

                if (currline.Contains("ADD") || currline.Contains("MOV") ||
                    currline.Contains("MUL") || currline.Contains("JMP") ||
                    currline.Contains("DEC") || currline.Contains("LOOP") ||
                    currline.Equals("") || currline.Contains("//") ||
                    currline.Contains(":") || currline.Contains("LOG") || currline.Contains("INC"))
                {
                    string[] befehle = currline.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    if (befehle.Length < 1)
                    {
                        MessageBox.Show("Syntaxfehler in Zeile " + currlineNo);
                        break;
                    }
                    switch (befehle[0])
                    {
                        case "ADD":
                            eax = eax + getRegister(befehle[1]);
                            break;
                        case "SUB":
                            eax = eax - getRegister(befehle[1]);
                            break;
                        case "MOV":
                            if (!(befehle.Length == 3))
                            {
                                MessageBox.Show("Syntaxfehler in Zeile " + currlineNo);
                                break;
                            }

                            switch (befehle[1].ToLower())
                            {
                                case "eax":
                                    eax = getRegister(befehle[2]);
                                    break;
                                case "ebx":
                                    ebx = getRegister(befehle[2]);
                                    break;
                                case "ecx":
                                    ecx = getRegister(befehle[2]);
                                    break;
                                case "edx":
                                    edx = getRegister(befehle[2]);
                                    break;
                                case "cx":
                                    cx = getRegister(befehle[2]);
                                    break;
                            }
                            break;
                        case "INC":
                            switch (befehle[1].ToLower())
                            {
                                case "eax":
                                    eax++;
                                    break;
                                case "ebx":
                                    ebx++;
                                    break;
                                case "ecx":
                                    ecx++;
                                    break;
                                case "edx":
                                    edx++;
                                    break;
                                case "cx":
                                    cx++;
                                    break;
                            }
                            break;
                        case "DEC":
                            switch (befehle[1].ToLower())
                            {
                                case "eax":
                                    eax--;
                                    break;
                                case "ebx":
                                    ebx--;
                                    break;
                                case "ecx":
                                    ecx--;
                                    break;
                                case "edx":
                                    edx--;
                                    break;
                                case "cx":
                                    cx--;
                                    break;
                            }
                            break;
                        case "MUL":
                            eax = eax * getRegister(befehle[1]);
                            break;
                        case "JMP":
                            currlineNo = findLine(befehle[1]);
                            break;
                        case "LOOP":
                            if (cx > 1)
                            {
                                currlineNo = findLine(befehle[1]);
                                cx--;
                            }
                            break;
                        case "LOG":
                            MessageBox.Show(getRegister(befehle[1]).ToString());
                            break;
                        case "":
                            break;
                    }

                    currlineNo++;
                }
                else
                {
                    MessageBox.Show("Unbekannter Befehl in Zeile " + currlineNo);
                    break;
                }

            } while (currlineNo <= lines.Length);

        }
    }
}