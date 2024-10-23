void PrintHeader(string topic, char ch = '-', int length = 80)
{
  int lengthLeft = (length - topic.Length - 2) / 2;
  int lengthRight = length - topic.Length - 2 - lengthLeft;

  if (lengthLeft >= 1 && lengthRight >= 1)
  {
    Console.WriteLine($"{new string(ch, lengthLeft)} {topic} {new string(ch, lengthRight)}");
  }
  else
  {
    Console.WriteLine(topic);
  }
}