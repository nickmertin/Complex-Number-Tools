using ComplexNumbers;
using System;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Complex_Number_Calculator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DecimalComplex left;
        char? cOp = null;
        string cReal = "", cImaginary = "";
        bool afterSeparator = false, afterDecimal = false, negative = false, replaceOp = false, input = true;

        public MainPage()
        {
            InitializeComponent();
            Focus(FocusState.Programmatic);
        }

        string render(string p1, string p2) => string.IsNullOrEmpty(p2) ? p1 : string.IsNullOrEmpty(p1) ? p2 + "i" : $"{p1} + {p2}i";

        void update() => display.Text = render(cReal, cImaginary);

        void updateResult() => display.Text = left.ToString();

        DecimalComplex current() => new DecimalComplex(string.IsNullOrEmpty(cReal) ? 0 : decimal.Parse(cReal), string.IsNullOrEmpty(cImaginary) ? 0 : decimal.Parse(cImaginary));

        void resetInput()
        {
            afterSeparator = false;
            afterDecimal = false;
            negative = false;
            replaceOp = false;
            cReal = "";
            cImaginary = "";
            display.Text = "0";
        }

        void type(char c)
        {
            input = true;
            if (replaceOp)
                resetInput();
            if (afterSeparator)
                cImaginary += c;
            else
                cReal += c;
            replaceOp = false;
            update();
        }

        void calc()
        {
            var right = current();
            switch (cOp)
            {
                case '+':
                    left += right;
                    break;
                case '-':
                    left -= right;
                    break;
                case '*':
                    left *= right;
                    break;
                case '/':
                    left /= right;
                    break;
                case '^':
                    left = DecimalComplex.Power(left, right);
                    break;
                case null:
                    if (input)
                        left = right;
                    break;
            }
            cOp = null;
            input = false;
        }

        void op(char c)
        {
            if (replaceOp)
            {
                cOp = c;
                return;
            }
            calc();
            cOp = c;
            replaceOp = true;
        }

        private void Digit0(object sender, RoutedEventArgs e) => type('0');

        private void Digit1(object sender, RoutedEventArgs e) => type('1');

        private void Digit2(object sender, RoutedEventArgs e) => type('2');

        private void Digit3(object sender, RoutedEventArgs e) => type('3');

        private void Digit4(object sender, RoutedEventArgs e) => type('4');

        private void Digit5(object sender, RoutedEventArgs e) => type('5');

        private void Digit6(object sender, RoutedEventArgs e) => type('6');

        private void Digit7(object sender, RoutedEventArgs e) => type('7');

        private void Digit8(object sender, RoutedEventArgs e) => type('8');

        private void Digit9(object sender, RoutedEventArgs e) => type('9');

        private void Decimal(object sender, RoutedEventArgs e)
        {
            if (!afterDecimal)
                type('.');
            afterDecimal = true;
        }

        private void Sign(object sender, RoutedEventArgs e)
        {
            if (afterSeparator)
                cImaginary = negative ? cImaginary.Substring(1) : "-" + cImaginary;
            else
                cReal = negative ? cReal.Substring(1) : "-" + cReal;
            negative = !negative;
            update();
        }

        private void Next(object sender, RoutedEventArgs e)
        {
            afterDecimal = false;
            afterSeparator = true;
        }

        private void Add(object sender, RoutedEventArgs e) => op('+');

        private void Subtract(object sender, RoutedEventArgs e) => op('-');

        private void Multiply(object sender, RoutedEventArgs e) => op('*');

        private void Divide(object sender, RoutedEventArgs e) => op('/');

        private void Backspace(object sender, RoutedEventArgs e)
        {
            if (replaceOp)
                return;
            if (afterSeparator)
                cImaginary = cImaginary.Substring(0, Math.Max(0, cImaginary.Length - 1));
            else
                cReal = cReal.Substring(0, Math.Max(0, cReal.Length - 1));
            update();
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            resetInput();
            left = 0;
        }

        private void ClearEntry(object sender, RoutedEventArgs e)
        {
            cReal = "";
            cImaginary = "";
            update();
        }

        private void Inverse(object sender, RoutedEventArgs e)
        {
            calc();
            left = 1 / left;
            cOp = null;
            updateResult();
        }

        private void Magnitude(object sender, RoutedEventArgs e)
        {
            calc();
            left = left.Magnitude;
            cOp = null;

            updateResult();
        }

        private void Power(object sender, RoutedEventArgs e) => op('^');

        private void Sqrt(object sender, RoutedEventArgs e)
        {
            calc();
            left = DecimalComplex.Sqrt(left);
            cOp = null;

            updateResult();
        }

        private void Calculate(object sender, RoutedEventArgs e)
        {
            calc();
            updateResult();
            replaceOp = true;
        }

        private void Page_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            switch (e.Key)
            {
                case VirtualKey.Number0:
                case VirtualKey.NumberPad0:
                    Digit0(null, null);
                    break;
                case VirtualKey.Number1:
                case VirtualKey.NumberPad1:
                    Digit1(null, null);
                    break;
                case VirtualKey.Number2:
                case VirtualKey.NumberPad2:
                    Digit2(null, null);
                    break;
                case VirtualKey.Number3:
                case VirtualKey.NumberPad3:
                    Digit3(null, null);
                    break;
                case VirtualKey.Number4:
                case VirtualKey.NumberPad4:
                    Digit4(null, null);
                    break;
                case VirtualKey.Number5:
                case VirtualKey.NumberPad5:
                    Digit5(null, null);
                    break;
                case VirtualKey.Number6:
                case VirtualKey.NumberPad6:
                    Digit6(null, null);
                    break;
                case VirtualKey.Number7:
                case VirtualKey.NumberPad7:
                    Digit7(null, null);
                    break;
                case VirtualKey.Number8:
                case VirtualKey.NumberPad8:
                    Digit8(null, null);
                    break;
                case VirtualKey.Number9:
                case VirtualKey.NumberPad9:
                    Digit9(null, null);
                    break;
                case VirtualKey.Decimal:
                    Decimal(null, null);
                    break;
                case VirtualKey.Back:
                    Backspace(null, null);
                    break;
                case VirtualKey.Delete:
                    ClearEntry(null, null);
                    break;
                case VirtualKey.Escape:
                    Clear(null, null);
                    break;
                case VirtualKey.Add:
                    Add(null, null);
                    break;
                case VirtualKey.Subtract:
                    Subtract(null, null);
                    break;
                case VirtualKey.Multiply:
                    Multiply(null, null);
                    break;
                case VirtualKey.Divide:
                    Divide(null, null);
                    break;
                case VirtualKey.Q:
                    Sqrt(null, null);
                    break;
                case VirtualKey.P:
                    Power(null, null);
                    break;
                case VirtualKey.R:
                    Inverse(null, null);
                    break;
                case VirtualKey.Enter:
                    Calculate(null, null);
                    break;
                default:
                    if ((int)e.Key == 190)
                        Decimal(null, null);
                    if ((int)e.Key == 188)
                        Next(null, null);
                    if ((int)e.Key == 220)
                        Magnitude(null, null);
                    if ((int)e.Key == 192)
                        Sign(null, null);
                    break;
            }
            e.Handled = true;
        }
    }
}