using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using System.Drawing;

namespace IntraVision.Web.Mvc
{
    public enum TypeSymbols
    {
        All,
        Chars,
        Numbers
    }

    public class CAPTCHAImageResult : ActionResult
    {
        private Color BackGroundColor { get; set; }
        private Color RandomTextColor { get; set; }
        private int RandomTextLength { get; set; }
        private TypeSymbols RandomTextTypeSymbols { get; set; }
        private string CaptchaKey { get; set; }

        public CAPTCHAImageResult(Color backGroundColor, Color randomTextColor, int randomTextLength, TypeSymbols randomTextTypeSymbols, string captchaKey = "")
        {
            BackGroundColor = backGroundColor;
            RandomTextColor = randomTextColor;
            RandomTextLength = randomTextLength;
            RandomTextTypeSymbols = randomTextTypeSymbols;
            CaptchaKey = captchaKey;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            RenderCAPTCHAImage(context);
        }

        private void RenderCAPTCHAImage(ControllerContext context)
        {
            Bitmap objBMP = new System.Drawing.Bitmap(100, 30);
            Graphics objGraphics = System.Drawing.Graphics.FromImage(objBMP);
            string randomWord = SelectRandomWord(RandomTextLength, RandomTextTypeSymbols);

            var key = string.Format("CaptchaValidationText_{0}", CaptchaKey);
            HttpContext.Current.Session[key] = randomWord;
            objGraphics.Clear(BackGroundColor);


            // Instantiate object of brush with black color
            SolidBrush objBrush = new SolidBrush(RandomTextColor);

            Font objFont = null;
            int a;
            String myFont, str;

            //Creating an array for most readable yet cryptic fonts for OCR's
            // This is entirely up to developer's discretion
            String[] crypticFonts = new String[11];

            crypticFonts[0] = "Arial";
            crypticFonts[1] = "Verdana";
            crypticFonts[2] = "Comic Sans MS";
            crypticFonts[3] = "Impact";
            crypticFonts[4] = "Haettenschweiler";
            crypticFonts[5] = "Lucida Sans Unicode";
            crypticFonts[6] = "Garamond";
            crypticFonts[7] = "Courier New";
            crypticFonts[8] = "Book Antiqua";
            crypticFonts[9] = "Arial Narrow";
            crypticFonts[10] = "Estrangelo Edessa";

            //Loop to write the characters on image  
            // with different fonts.
            for (a = 0; a <= randomWord.Length - 1; a++)
            {
                myFont = crypticFonts[new Random().Next(a)];
                objFont = new Font(myFont, 14, FontStyle.Bold | FontStyle.Italic | FontStyle.Strikeout);
                str = randomWord.Substring(a, 1);
                objGraphics.DrawString(str, objFont, objBrush, a * 20, 0);
                objGraphics.Flush();
            }
            context.HttpContext.Response.ContentType = "image/GF";
            objBMP.Save(context.HttpContext.Response.OutputStream, ImageFormat.Gif);
            objFont.Dispose();
            objGraphics.Dispose();
            objBMP.Dispose();

        }

        private string SelectRandomWord(int numberOfChars, TypeSymbols typeChars)
        {
            if (numberOfChars > 36)
            {
                throw new InvalidOperationException("Random Word Charecters can not be greater than 36.");
            }
            // Creating an array of 26 characters  and 0-9 numbers
            var symbols = new List<char>();
            //char[] columns = new char[36];

            switch (typeChars)
            {
                case TypeSymbols.All:
                    {
                        for (int charPos = 65; charPos < 65 + 26; charPos++)
                            symbols.Add((char)charPos);

                        for (int intPos = 48; intPos <= 57; intPos++)
                            symbols.Add((char)intPos);

                        break;
                    }
                case TypeSymbols.Chars:
                    {
                        for (int charPos = 65; charPos < 65 + 26; charPos++)
                            symbols.Add((char)charPos);

                        break;
                    }
                case TypeSymbols.Numbers:
                    {
                        for (int intPos = 48; intPos <= 57; intPos++)
                            symbols.Add((char)intPos);

                        break;
                    }
            }

            StringBuilder randomBuilder = new StringBuilder();


            Random randomSeed = new Random();
            for (int incr = 0; incr < numberOfChars; incr++)
                randomBuilder.Append(symbols.ElementAt(randomSeed.Next(symbols.Count)).ToString());

            return randomBuilder.ToString();
        }
    }
}