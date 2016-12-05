using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFluent;
using NUnit.Framework;

namespace WordWrapTests
{
    public class WordWrapTest
    {
        [Test]
        public void Should_return_empty_string_when_column_is_zero()
        {
            Check.That(WordWrap.WrapLine("test_string", 0)).IsEqualTo("test_string");
        }

        [Test]
        public void Should_split_string_with_blank_caracter()
        {
            Check.That(WordWrap.WrapLine("ab cd", 2)).IsEqualTo("ab\ncd");
        }

        [Test]
        public void Should_split_word_when_unable_to_split_blank()
        {
            Check.That(WordWrap.WrapLine("ab cd", 4)).IsEqualTo("ab\ncd");
        }

        [Test]
        public void Should_split_string_with_words_of_same_size_into_multiple_lines()
        {
            Check.That(WordWrap.WrapLine("ab cd ef gh", 2)).IsEqualTo("ab\ncd\nef\ngh");
        }

        [Test]
        public void Should_split_words_of_same_sizes_into_multiple_lines()
        {
            Check.That(WordWrap.WrapLine("abc cd efg gh", 3)).IsEqualTo("abc\ncd\nefg\ngh");
        }

        [Test]
        public void Should_split_words_of_two_different_sizes_into_multiple_lines_containing_more_than_one_word()
        {
            Check.That(WordWrap.WrapLine("abc cd efg gh", 6)).IsEqualTo("abc cd\nefg gh");
        }

        [Test]
        public void  Should_split_words_of_multiple_different_sizes_into_multiple_lines_containing_more_than_one_word()
        {
            Check.That(WordWrap.WrapLine("abcde fg efg a abcd", 6)).IsEqualTo("abcde\nfg efg\na abcd");
        }


    [Test]
        public void Should_do_when_condition()
        {
            Check.That(
                WordWrap.WrapLine(
                    "The Romans were a clever bunch. They conquered most of Europe and ruled it for hundreds of", 10))
                .IsEqualTo("The Romans\nwere a\nclever\nbunch.\nThey\nconquered\nmost of\nEurope and\nruled it\nfor\nhundreds\nof");

        }

    }

    public static class WordWrap
    {
        public static string WrapLine(string inputString, int column)
        {
            if (column == 0 || inputString.Length <= column)
                return inputString;
            bool isSpace = inputString[column] == ' ';
            int indexToSplit = column;
            if (!isSpace)
                indexToSplit = inputString.Substring(0, column).LastIndexOf(' ');
            bool isLastIndex = inputString.Length <= column;
            string inputStringLeft = inputString.Substring(0, indexToSplit).TrimEnd() + (isLastIndex? "" : "\n");
            return inputStringLeft+ WrapLine(inputString.Substring(indexToSplit).TrimStart(), column);
        }
    }
}
