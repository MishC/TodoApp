using System;
using System.Text;
namespace TodoApp.Classes
{
    public class EmojiService
    {
        public int HighSurrogate;
        public int? LowSurrogate;

        public byte[] GetEmojiBytes()
        {
            char[] emojiChars;

            if (LowSurrogate.HasValue)
            {
                emojiChars = new char[] { (char)HighSurrogate, (char)LowSurrogate.Value };
            }
            else
            {
                emojiChars = new char[] { (char)HighSurrogate };
            }

            byte[] bytes = Encoding.Unicode.GetBytes(emojiChars);

            return bytes;
        }
    }
}