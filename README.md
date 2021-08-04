# CSharp Numeral Converter
***CSharp Numeral Converter*** - это мой дебютный проект на языке [C#](https://ru.wikipedia.org/wiki/C_Sharp) и платформе [.NET](https://en.wikipedia.org/wiki/.NET_Framework) ([.NET documentation](https://docs.microsoft.com/en-us/dotnet/)) в целом.

Проект безусловно интересный и полезный, именно в качестве первого 1️⃣ т.к. при работе над ним, я получил опыт, практику работы с базовыми и не очень элементами языка программирования [C#](https://docs.microsoft.com/en-us/dotnet/csharp/), например, почти со всеми типами данных, преобразованием типов, массивами, строками, методами, циклами, условными операторами(неожиданно, блин 😂🤣) и т.д. 

### Было и несколько любопытных моментов: 

* Как разделить введённую строку на дробную и целую части: 
  
  ```
  string[] parts = number.Split('.', ',');

  wholePart = Convert.ToString(parts[0]);
  fractionalPart = Convert.ToString(parts[1]);
  ```
  
* Как работать с буквами систем счисления при переводе в 10-ую СС

  ```
  
  int item = (int)num[i];

  if (item >= 97 & item <= 122)          // работа с таблицей ascii; решает проблему если ввели маленькую букву
  {
      item -= 32;
  }

  int itemNum;
  itemNum = item - 48;

  if (itemNum >= 10)                     // перевод цифры в букву
      itemNum -= 7;
  }
 
  ```
  ![](https://lh3.googleusercontent.com/proxy/43ADxOKH7uYNxZHSEZEOlo92PXQ0ndKwYZl0lSaB6ed07OQEAfgNYp1DgJ53_24ZV12qH4_4hwmyQg) 
  
  Например символ `'A'` после явного преобразования `int item = (int)num[i];` в тип `int` будет равняться `65` по таблице ascii
  
  * Вычитаем из данного числа 48 
  * Если число меньше 10, значит от строки мы "откусили" цифру, 
  * Если число больше 10, значит это буква и следует вычесть ещё -7, чтобы получить альтернативный код этой буквы в 10-ой системе счисления. 
  
    В данном случае `65 - 48 = 17`, значит это буква, следовательно `17 - 7 = 10`
    
* Как работать с буквами систем счисления при переводе из 10-ой СС в другую
  ```
  item = Convert.ToString((char)(numItem + 55));
  ```
  * Основан на том же принципе работы с таблицой ascii, что и в примере выше 👆
  
  
* Перевод дробной части числа в 10-ую СС
  ```
  DecNumber += Convert.ToDouble(arr[i]) * Math.Pow(BaseNumber, n * (-1));     
  ```
  * `n*(-1)` добавлен для создания отрицательной степени, этого требует [алгоритм](https://ege-study.ru/ege-informatika/sistemy-schisleniya-perevod-iz-odnoj-sistemy-v-druguyu/) создания дробной части числа
  
  
* Удаление `0` из строки-результата перевода дробной части числа
```
char[] MyChar = { '0' };                   
string newP2 = p2.TrimStart(MyChar);
```
  * Массив `MyChar` содержит в себе символы, которые метод `TrimStart` принимает и удаляет из начала строки

Для реализации данных моментов пришлось обращаться к [документации Microsoft](https://docs.microsoft.com/en-us/dotnet/csharp/) (ссылки на документацию: [documentation1](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/), [documentation2](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/)), к форумам 👨‍💻, к листку бумаги 📄, ручке ✏, кружке с чаем ☕ и думалке 🧠. 

## Теория. Что нужно знать для понимания

Также данный проект интересен с точки зрения “теоретической, научной” информатики т.к. связан с [системами счисления](https://ru.wikipedia.org/wiki/%D0%A1%D0%B8%D1%81%D1%82%D0%B5%D0%BC%D0%B0_%D1%81%D1%87%D0%B8%D1%81%D0%BB%D0%B5%D0%BD%D0%B8%D1%8F) (СС). 
#### Для понимания необходимо: ####
* Знать что такое система счисления и как строить [системы счисления](https://ru.wikipedia.org/wiki/%D0%A1%D0%B8%D1%81%D1%82%D0%B5%D0%BC%D0%B0_%D1%81%D1%87%D0%B8%D1%81%D0%BB%D0%B5%D0%BD%D0%B8%D1%8F)
* Понимать "бумажный" [алгоритм](https://ege-study.ru/ege-informatika/sistemy-schisleniya-perevod-iz-odnoj-sistemy-v-druguyu/) перевода (конвертации) чисел между СС
* Знать что такое [таблица ascii](https://ru.wikipedia.org/wiki/ASCII)  
* Базовый синтаксис `C#`
* Хорошо разбираться в преобразовании [типов данных](https://docs.microsoft.com/ru-ru/dotnet/csharp/fundamentals/types/) ([documentation](https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/builtin-types/built-in-types))

## Св-ва проекта:
* Время выполнения (чистое время): 11 часов

* Сложность: `easy` по задумке и на первый взгляд, но мне хочется дать `medium` (проект оказалься мудрённее, чем я думал именно со стороны реализации на `C#`)

* Моя оценка по 5-ти бальной шкале: `⭐⭐⭐⭐⭐`

## Особенности, моменты, на которые хотелось бы обратить внимание: 

* Все возможные СС 1-36. Ограничения связаны с всего лишь 10-ю цифрами и английским алфавитом в 26 символов, он просто закончился 😁

* Реализован перевод дробных чисел

* Проверки на корректность пользовательского ввода оснований систем счисления

## P.S.
* Проект будет развиваться не бесконечно, но до определённого момента, а именно, приложения под `android`.
* Точных дат называть не буду т.к. их пока и сам не знаю. Потихоньку, помаленьку... 🐌
* Если я смог помочь кому-то этим проектом я рад 😉 т.к. сам помню искал алгоритмы, не находил... эх...
---

### Спасибо за внимание!
