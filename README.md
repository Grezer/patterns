# Паттерны

## Порождающие

### Создание объектов. Нужны, что бы объекты точно удалялись (не оставались как балласт) и не удалялись раньше времени

1. [Singleton](https://github.com/Grezer/patterns#singleton)
2. [Factory method](https://github.com/Grezer/patterns#factory-method-virtual-constructor)
3. [Prototype](https://github.com/Grezer/patterns#prototype)
4. [Abstract factory](https://github.com/Grezer/patterns#abstract-factory-kit)
5. [Builder](https://github.com/Grezer/patterns#builder)

## Структурные

### Создание сложных объектов из простых. При этом могут использоваться следующие механизмы:

- Наследование, когда базовый класс определяет интерфейс, а подклассы - реализацию. Структуры, на основе наследования, получаются статическими.
- Композиция, когда структуры строятся путем объединения объектов некоторых классов. Композиция позволяет получать структуры, которые можно изменять во время выполнения.

1. [Adapter](https://github.com/Grezer/patterns#adapter-wrapper)
2. [Composite](https://github.com/Grezer/patterns#composite)
3. [Decorator](https://github.com/Grezer/patterns#decorator-wrapper)
4. [Proxy](https://github.com/Grezer/patterns#proxy-surrogate)
5. [Facade](https://github.com/Grezer/patterns#facade)
6. [Bridge](https://github.com/Grezer/patterns#bridge-handlebody)
7. [Flyweight](https://github.com/Grezer/patterns#flyweight)

## Поведенческие

### Изменение поведения объектов в run time (объект должен по разному себя вести в разные моменты времени)

1. [Chain of Responsibility](https://github.com/Grezer/chain-of-responsibility)
2. [Command](https://github.com/Grezer/command-action--transaction)
3. [Iterator](https://github.com/Grezer/iterator-cursor)
4. Observer
5. State
6. Strategy
7. Mediator
8. Memento
9. Template method
10. Visitor

## Cистемные: допишу

## Принципы ООП (SOLID): допишу

## Словечки

Low coupling - низкая связность. Такая конструкция, которая слабо связана (неизменяемость интерфейсов и инкапсуляция классов).

---

## Singleton

Создание класса без публичных констркторов.  
Применяется когда нужен класс, который:

- гарантированно имел бы <b>ОДИН</b> экземпляр
- этот экземпляр должен быть доступен в любой точке приложения

### [Refactoring guru](https://refactoring.guru/ru/design-patterns/singleton)

### Назначение

> Гарантирует, что у класса есть только один экземпляр, и предоставляет к нему глобальную точку доступа

### Мотивация

> Для многих программных систем иногда нужно, чтобы существовал единственный экземпляр класса. Например оконный менеджер. Как гарантировать единственность и доступность этого экземпляра?

### Структура

<p align="center">
  <img width="471" height="400" src="./resources/singleton/struct.png">
</p>

### Реализация

```C#
// Пример класса
public class Singleton
{
    private Singleton() {}
    private static Singleton_instance = null;
    public void DoSome() {}

    public static Singleton GetInstance()
    {
        if(_instance == null)
            _instance = new Singleton();
        return _instance
    }
}

// Пример использования
Singleton single = Singleton.GetInstance();
single.DoSome();
```

### Разультаты

- (+) Гарантируется единственность экземпляра класса и его глобальная доступность.

- (-) Может создавать проблемы в параллельных и/или распределенных приложениях. В этих случаях необходимо более сложное решение.

---

## Factory method (Virtual Constructor)

Синоним: Virtual Constructor

Применяется, когда есть множество разнородных объектов, например, много фигур: прямоугольник, треугольник, элипс и т.д.

### [Refactoring guru](https://refactoring.guru/ru/design-patterns/factory-method)

### Назначение

> Определяет интерфейс для создания объекта, оставляя подклассам решение о том, какой класс инстанцировать.

### Мотивация

> Рассмотрим пример с редактором векторной графики.  
> Все фигуры, доступные в данном приложении являются объектами подклассов базового класса Figure, в нашем примере – Rectangle, Ellipse и Romb.  
> ![](./resources/factory_method/classes.png)  
> Здесь пользователь сначала выбирает тип порождаемой фигуры в инструментальном меню, а потом по клику в рабочей области создается экземпляр соответствующего класса.  
> Тогда код метода <i>panel_MouseDown</i> будет следующим:

```C#
Figure f = null;
switch (selection)
{
    case 1:
        f = new Rectangle();
        break;
    case 2:
        f = new Ellipse();
        break;
}
```

> Многие современные программы работают с плагинами – кодом, добавляемым во время исполнения программы.  
> Как добавить новую фигуру во время исполнения?

### Решение

Создание параллельно с иерархией классов Figure, иерархию классов FigureCreator.  
![](./resources/factory_method/creators.png)

```C#
class FigureCreator
{
    public virtual Figure CreateFigure()
    {
        return null;
    }
}

// Creator прямоугольника
class RectangleCreator : FigureCreator
{
    public override Figure CreateFigure()
    {
        return new Rectangle();
    }
}

// Creator элипса
class EllipseCreator : FigureCreator
{
    public override Figure CreateFigure()
    {
        return new Ellipse();
    }
}
```

Теперь код метода panel_MouseDown будет более управляемый, где currCreator соответствующий экземпляр класса FigureCreator, инициируемый при нажатии на кнопку выбора создаваемой фигуры.  
Для добавления новой фигуры надо:

- написать класс, наследник Figure,
- написать класс, наследник FigureCreator,
- добавить новую кнопку на панель и код инициализации этого «создателя» на эту кнопку.

```C#
Figure f = null;
if (currCreator != null)
    f = currCreator.CreateFigure();
```

### Структура

![](./resources/factory_method/struct.png)

### Участники

<b>Product</b> – базовый класс для семейства конкретных продуктов, экземпляры которых должны инстанцироваться.  
<b>Creator</b> – базовый класс для семейства «создателей», классы реализующие метод, создающий новый экземпляр соответствующего класса.  
Для каждого класса ConcreteProduct должен быть соответствующий класс ConcreteCreator, задача которого изготавливать экземпляры класса ConcreteProduct.

### Разультаты

- (+) Снижает зависимость между классами. В нашем примере редактор работает только с экземплярами класса Figure, экземпляры конкретных классов создаются с помощью «создателя».

- (+) Решает задачу "разрывающую" два разных события:
  1. Выбор "кого мы делаем" (прямоугольник/треугольник/элипс)
  2. Изготовление этого экземпляра

---

## Prototype

Применяется, когда нужно создавать объекты одного класса, различающиеся только свойствами (копировать).

### [Refactoring guru](https://refactoring.guru/ru/design-patterns/prototype)

### Назначение

> Создание нового объекта путем клонирования существующего прототипа.

### Мотивация

> В качестве примера рассмотрим нотный редактор.  
> В качестве рабочей области в нем будет нотный стан, а в качестве инструментальной линейки – набор нот разной высоты и длительности.  
> Основным объектом в такой программе будет нота – класс Note.  
> <b>Вопрос</b>: как создавать новые экземпляры этого класса? Что делать по клику в инструментальной панели?  
> <b>Решение</b>: добавить в класс Note метод Clone(), создающий точную копию объекта.  
> С каждой кнопкой связать экземпляр класса Note, копию которого и вставлять в программу.

> Второй пример – группировка различных фигур в векторном редакторе.  
> <b>Вопрос</b>: Если вы сгруппировали интересную для вас композицию (точка, точка, два кружочка …), то как ее поместить на инструментальную панель?  
> Точнее – что делать при нажатии на эту кнопку и как создать новый экземпляр этой группы?  
> <b>Решение</b>: как и в предыдущем случае добавим в базовом классе Figure метод Clone, создающий копию объекта.  
> Соответствующий кнопке «создатель» будет иметь копию группы как прототип создаваемого объекта, и в методе CreateFigure – создавать и возвращать копию своего прототипа.

### Структура

![](./resources/prototype/struct.png)

### Участники

<b>Client</b> – класс, использующий этот паттерн. В нашем случае, например, векторный редактор.  
Он содержит ссылку на прототип, который может быть инстанцирован объектом любого класса – наследника Prototype.  
<b>Prototype</b> – базовый абстрактный класс для семейства классов, поддерживающих операцию Clone(), метод позволяющий получить копию объекта.  
<b>ConcretePrototype1, ConcretePrototype2</b> – классы наследующие Prototype и реализующие метод Clone.

### Реализация

<b>Основная проблема</b> в реализации метода Clone – определить глубокое (deep) или мелкое (shallow) копирование объекта.

Общий подход к реализации метода простой:  
Надо создать новый экземпляр и скопировать туда значение полей объекта.  
Разница возникает, когда поле объекта содержит ссылку на другой объект какого-то класса.

- Если дубликат получает копию ссылки, то это мелкое копирование.
- Если ссылаемый объект, в свою очередь, дублируется и дубликат получает ссылку на копию объекта, то это глубокое копирование.

<b>Второй вопрос</b> – как управлять множеством прототипов.

Для этого логично завести менеджер прототипов – коллекцию прототипов, каждый объект в котором идентифицируется своим ключом.  
Client запрашивает нужный ему прототип по ключу перед клонированием.

### Разультаты

- (+) Ослабление связности.

- (+) Возможно добавление и удаление прототипов во время выполнения.

- (+) Спецификация новых прототипов путем изменения значений.

- (+) Динамическое конфигурирование приложения набором прототипов.

---

## Abstract factory (kit)

Применяется, когда нужно порождать много разных объектов, но из одного семейства, например: разные коллекции кухонной мебели (столы, шкафы, стулья).

### [Refactoring guru](https://refactoring.guru/ru/design-patterns/abstract-factory)

### Назначение

> Определяет интерфейс для создания семейств взаимосвязанных или взаимозависимых объектов, не специфицируя их конкретных классов.

### Мотивация

> В качестве классического примера рассмотрим переносимую библиотеку GUI.  
> Пусть некоторое приложение с поддержкой графического интерфейса пользователя рассчитано на использование на различных платформах, при этом внешний вид этого интерфейса должен соответствовать принятому стилю для той или иной платформы.  
> Например, если это приложение установлено на Windows-платформу, то его кнопки, меню, полосы прокрутки должны отображаться в стиле, принятом для Windows.  
> Группой взаимосвязанных объектов в этом случае будут элементы графического интерфейса пользователя для конкретной платформы.

> Другой пример.  
> Рассмотрим текстовый редактор с многоязычной поддержкой, у которого имеются функциональные модули, отвечающие за расстановку переносов слов и проверку орфографии.  
> Если, скажем, открыт документ на русском языке, то должны быть подключены соответствующие модули, учитывающие специфику русского языка.  
> Ситуация, когда для такого документа одновременно используются модуль расстановки переносов для русского языка и модуль проверки орфографии для немецкого языка, исключается.  
> Здесь группой взаимосвязанных объектов будут соответствующие модули, учитывающие специфику некоторого языка.

### Реализация

Для решения задачи по созданию семейств взаимосвязанных объектов паттерн Abstract Factory вводит понятие абстрактной фабрики.  
Абстрактная фабрика представляет собой некоторый полиморфный базовый класс, назначением которого является объявление интерфейсов фабричных методов, служащих для создания продуктов всех основных типов (один фабричный метод на каждый тип продукта).  
Производные от него классы, реализующие эти интерфейсы, предназначены для создания продуктов всех типов внутри семейства или группы.

### Структура

![](./resources/abstract_factory/struct.jpg)

### Разультаты

- (+) Скрывает сам процесс порождения объектов, а также делает систему независимой от типов создаваемых объектов, специфичных для различных семейств или групп (пользователи оперируют этими объектами через соответствующие абстрактные интерфейсы).

- (+) Позволяет быстро настраивать систему на нужное семейство создаваемых объектов.  
  В случае многоплатформенного графического приложения для перехода на новую платформу, то есть для замены графических элементов (кнопок, меню, полос прокрутки) одного стиля другим достаточно создать нужный подкласс абстрактной фабрики.  
  При этом условие невозможности одновременного использования элементов разных стилей для некоторой платформы будет выполнено автоматически.

- (-) Трудно добавлять новые типы создаваемых продуктов или заменять существующие, так как интерфейс базового класса абстрактной фабрики фиксирован.

---

## Builder

Применяется, когда нужно порождать объекты, требующие пошаговой инициализации множества полей и вложенных объектов.

### [Refactoring guru](https://refactoring.guru/ru/design-patterns/builder)

### Назначение

> Отделяет создание сложного объекта от его представления, так что в результате одного и того же процесса создания могут получаться разные представления.

### Мотивация

> Предположим, что вы разрабатываете супер текстовый редактор, позволяющий делать сверхсложные и очень красивые документы.  
> Нужно предусмотрить вывод готового документа в любой из стандартных форматов – PDF, в виде HTML и в виде Microsoft Word документа.  
> Таким образом в главном меню вашего редактора должен быть пункт Save As, где в качестве подпунктов – as PFD document, as HTML, as Microsoft Word.  
> <b>Вопрос</b>: как будет выглядеть реализация этих пунктов меню?  
> Первое, что приходит в голову – фабричные методы типа следующих:

```C#
private PDFDocument MakePDFDocument(MySuperDocument myDoc)          { ... }
private HTMLDocument MakeHTMLDocument(MySuperDocument myDoc)        { ... }
private MSWordDocument MakeMSWordDocument(MySuperDocument myDoc)    { ... }
```

> При таком подходе сильная связность между сложными сущностями. Хотелось бы ослабить эту связь.
> Вместо того, чтобы создавать новый тип за один шаг, предлагается делать его по шагам. Аналогично тому, как строится дом.  
> Сначала фундамент, потом первый этаж, потом второй и т.д. После этого крыша. После – отделка, внутренние коммуникации и т.п.  
> Документ тоже имеет структуру и состоит из составных частей разного уровня: абзацы, картинки, таблицы, параграфы и главы и т.д.

### Реализация

Builder определяет алгоритм поэтапного создания продукта в специальном классе Director (распорядитель), а ответственность за координацию процесса сборки отдельных частей продукта возлагает на иерархию классов Builder.  
В этой иерархии базовый класс Builder объявляет интерфейсы для построения отдельных частей продукта, а соответствующие подклассы ConcreteBuilder их реализуют подходящим образом, например, создают или получают нужные ресурсы, сохраняют промежуточные результаты, контролируют результаты выполнения операций.

### Структура

![](./resources/builder/struct.jpg)

Класс Director содержит указатель или ссылку на Builder, который перед началом работы должен быть сконфигурирован экземпляром ConcreteBuilder, определяющим соответствующе представление.  
После этого Director может обрабатывать клиентские запросы на создание объекта.  
Получив такой запрос, с помощью имеющегося экземпляра строителя Director строит продукт по частям, а затем возвращает его пользователю.

![](./resources/builder/struct2.jpg)

### Разультаты

- (+) Возможность контролировать процесс создания сложного продукта.

- (+) Возможность получения разных представлений некоторых данных.

- (-) ConcreteBuilder и создаваемый им продукт жестко связаны между собой, поэтому при внесении изменений в класс продукта скорее всего придется соответствующим образом изменить и класс ConcreteBuilder.

---

## Adapter (Wrapper)

### [Refactoring guru](https://refactoring.guru/ru/design-patterns/adapter)

### Назначение

> Паттерн, позволяющий преобразовать интерфейс существующего класса в интерфейс требуемого класса.

### Мотивация

> Для примера рассмотрим программу – векторный редактор.  
> Все графические объекты в этой программе – наследники класса Figure.  
> <b>Вопрос</b>: можно ли использовать в этой программе объекты класса, не являющегося наследником Figure, но являющегося графическими объектами.  
> <b>Решение</b>: создать класс – наследник Figure: LinkToFlower, cсодержащий ссылку на экземпляр класса Flower.  
> Задача класса LinkToFlower – реализовать все методы класса Figure, используя методы класса Flower.  
> Шаблон Adapter – обобщение этого решения.

![](./resources/adapter/struct.jpg)

### Структура

- <b>Client</b> – класс, использующий экземпляр класса Target (приложение)
- <b>Target</b> – базовый (абстрактный) класс для группы классов
- <b>ConcreteTarget</b> – конкретный класс, наследник Target
- <b>Adapter</b> – наследник Target, содержащий ссылку на экземпляр Adaptee
- <b>Adaptee</b> – адаптируемый класс

![](./resources/adapter/struct2.jpg)

### Реализация

Сложность реализации зависит от степени удаленности адаптируемого класса Adaptee от требуемого класса Target.  
<b>Важный случай</b> – адаптер к семейству классов. Если, например, адаптируемый класс Flower является наследником класса GenericFigure, являющегося базовым классом для семейства ряда графических объектов, можно реализовать адаптер для всего семейства, сделав его для базового класса.  
Возникает сложность – создание конкретного адаптируемого объекта.

### Разультаты

- (+) Повышает гибкость, позволяя использовать классы, не входящие в требуемую иерархию.

- (+) Ослабляет связь между клиентом и фактически используемым классом.

---

## Composite

### [Refactoring guru](https://refactoring.guru/ru/design-patterns/composite)

### Назначение

> Компонует объекты в древовидные структуры для представления иерархий <i>часть-целое</i>. Позволяет единообразно рассматривать как индивидуальные, так и составные объекты.

### Мотивация

> Для примера рассмотрим программу – векторный редактор.  
> Все графические объекты в этой программе – наследники класса Figure.  
> Любой приличный редактор позволяет группировать объекты в группы, что дает возможность манипулировать такой группой как обычной фигурой.
> Понятно, что нужен новый класс – наследник Figure, который будет содержать список фигур, входящих в группу.  
> Возникает несколько вопросов:
>
> - Как отличить простую фигуру от составной?
> - Как определить значения полей базового класса: координаты левого верхнего угла, ширина, высота для группы фигур?

![](./resources/composite/struct.jpg)

### Структура

- <b>Component</b> – базовый класс для семейства компонентов
- <b>IСomposite</b> – интерфейс для составных объектов
- <b>Composite</b> – составной компонент, содержащий список компонентов

![](./resources/composite/struct2.jpg)

### Реализация

Для нашего примера интерфейс IGroup мог бы выглядеть так:

```C#
interface IGroup
{
    void Add(Figure f);
    void Remove(Figure f);
    Figure GetItem(int indx);
}
```

Для проверки, является ли конкретный объект сложным или простым, надо проверить:

```C#
if (fig is IGroup)
```

Для более общего случая лучше сделать обобщенный интерфейс:

```C#
interface IGroup <T>
{
    void Add(T f);
    void Remove(T f);
    T GetItem(int indx);
}
```

### Разультаты

- (+) Позволяет построить единую иерархию классов, состоящих как из элементарных, так и составных объектов.

- (+) Упрощает архитектуру клиента. Позволяет клиентской программе работать единообразно со всеми объектами, кроме тех случаев, когда нужно различать составные и элементарные объекты (например операция Ungroup).

- (+) Облегчается добавление новых компонентов.

---

## Decorator (Wrapper)

### [Refactoring guru](https://refactoring.guru/ru/design-patterns/decorator)

### Назначение

> Динамически добавляет объекту новые обязанности.  
> Является гибкой альтернативой порождению подклассов с целью расширения обязанностей.  
> Используется в первую очередь для добавления функционала.

### Мотивация

> Иногда нужно добавить новые дополнительные обязанности конкретному объекту, а не классу в целом.  
> Конечно, есть стандартный способ – создать подкласс, добавив в него дополнительную функциональность.  
> Стандартный пример – текстовый компонент в графическом редакторе.  
> Стандартный компонент позволяет поместить текст внутри компонента.  
> Сам компонент может перемещаться внутри картинки, можно менять шрифт, его цвет и т.п.  
> Дополнительная функциональность:
>
> - хочется иметь возможность включить рамку вокруг текста, с возможностью менять ее толщину, цвет, стиль и т.п.
> - хочется иметь прокрутку (Scroll), что позволяет поместить большой текст внутри небольшого компонента и прокручивать его содержимое.
>
> В стандартном подходе понадобятся три наследника класса TextView: BorderTextVew, ScrollTextView, BorderScrollTextView для текста с рамкой, с прокруткой и с рамкой вместе с прокруткой. А если надо добавить еще одно украшение?

### Идея

> Идея декоратора проста – мы просто создаем класс, наследник базового с требуемой дополнительной функциональностью.  
> Но основную функциональность перекладываем на экземпляр класса, интегрированный в объект.  
> Например для рамки заведем класс BorderDecoratorTextView.

```C#
class BorderDecoratorTextView : TextView
{
    TextView view;

    public override void Draw()
    {
        // draw Border
        view.Draw();
    }
}
```

### Структура

- <b>Component</b> – базовый класс для семейства компонентов
- <b>ConcreteComponent</b> – конкретный класс компонента
- <b>Decorator</b> – базовый класс для декораторов
- <b>ConcDecorator1</b> – декоратор с добавленной функциональностью (методом)
- <b>ConcDecorator2</b> – декоратор с добавленным состоянием.

![](./resources/decorator/struct.jpg)

### Реализация

- Класс Decorator использует отношение композиции.
- Указатель на декорируемый объект инициализируется в конструкторе.
- Класс Decorator делегирует выполнение операции декорируемому объекту.
- Для реализации каждой дополнительной функциональности создайте класс, производный от Decorator.
- Подкласс Decorator реализует дополнительную функциональность и делегирует выполнение операции базовому классу Decorator.
- Клиент несет ответственность за конфигурирование системы: устанавливает типы и последовательность использования основного объекта и декораторов.

### Разультаты

- (+) Большая гибкость, чем у статического наследования.

- (+) Позволяет избежать перегруженных методами классов на верхних уровнях иерархии.

- (-) Декоратор и его компонент, вообще говоря, не идентичны.

- (-) Порождает большое число мелких объектов.

---

## Proxy (surrogate)

### [Refactoring guru](https://refactoring.guru/ru/design-patterns/proxy)

### Назначение

> Является суррогатом другого объекта и контролирует доступ к нему.

> <b>Заместитель</b> — это структурный паттерн проектирования, который позволяет подставлять вместо реальных объектов специальные объекты-заменители.  
> Эти объекты перехватывают вызовы к оригинальному объекту, позволяя сделать что-то до или после передачи вызова оригиналу.

### Когда следует использовать?

1. Виртуальный proxy является заместителем объектов, создание которых обходится дорого. Реальный объект создается только при первом запросе/доступе клиента к объекту.
2. Удаленный proxy предоставляет локального представителя для объекта, который находится в другом адресном пространстве ("заглушки" в RPC и CORBA).
3. Защитный proxy контролирует доступ к основному объекту. "Суррогатный" объект предоставляет доступ к реальному объекту, только если вызывающий объект имеет соответствующие права.
4. Интеллектуальный proxy выполняет дополнительные действия при доступе к объекту.

### Мотивация

> Вам нужно управлять ресурсоемкими объектами.  
> Вы не хотите создавать экземпляры таких объектов до момента их реального использования.  
> Суррогат или заместитель это объект, интерфейс которого идентичен интерфейсу реального объекта.  
> При первом запросе клиента заместитель создает реальный объект, сохраняет его адрес и затем отправляет запрос этому реальному объекту.  
> Все последующие запросы просто переадресуются инкапсулированному реальному объекту

### Структура

- <b>Subject</b> – базовый класс
- <b>RealSubject</b> – реальный класс субъекта
- <b>Proxy</b> – заместитель, хранит ссылку на реальный субъект. Реальные обязанности зависят от назначения заместителя

![](./resources/proxy/struct.png)

### Реализация

- Определяется назначением заместителя.
- Для отложенной инициализации (виртуальный заместитель) – откладывает создание субъекта до прямого обращения к нему.
- Защищающий заместитель (Secure Stub) проверяет, имеет ли вызывающий необходимые для запроса права.
- Удаленный заместитель (Stub) отвечает за упаковку и передачу запроса реальному субъекту в другом адресном пространстве, а также за получение, распаковку и возращение результата.

### Разультаты

- (+) Удаленный заместитель скрывает тот факт, что субъект находится в другом адресном пространстве (или на другом континенте).

- (+) Виртуальный заместитель может выполнит оптимизацию, например отложить создание субъекта до первого требования.

- (+) Защищающий заместитель и «умная» ссылка позволяют решить дополнительные задачи при доступе к объекту (например copy_on_write механизм).

---

## Facade

### [Refactoring guru](https://refactoring.guru/ru/design-patterns/facade)

### Назначение

> Предоставляет унифицированный интерфейс некоторой подсистемы.

### Мотивация

> При разбиении сложной системы на подсистемы возникает задача – свести к минимуму зависимость одной подсистемы от другой.  
> Фасад – один из способов решения этой задачи, предоставляет единый интерфейс для доступа к функциям подсистемы.  
> Например API Unix – является фасадом операционной системы для программиста.  
> Набор функций OpenGL – фасад большой библиотеки трехмерной графики.

### Структура

- <b>Facade</b> – фасад подсистемы
- <b>Elem1, Elem2, …</b> – элементы подсистемы

![](./resources/facade/struct.png)

### Реализация

- Ключевой вопрос реализации – определение интерфейса фасада.
- Это определяет удобство и гибкость работы с подсистемой.
- Сравните для примера API Unix и Windows

### Разультаты

- (+) Изолирует клиента от деталей реализации подсистемы, что ослабляет связность.

- (+) Фасад не препятствует получение клиентом прямой ссылки на внутренний объект подсистемы (через метод фасада) для увеличения эффективности взаимодействия.

---

## Bridge (Handle/Body)

### [Refactoring guru](https://refactoring.guru/ru/design-patterns/bridge)

### Назначение

> Отделяет абстракцию от реализации, так чтобы и то и другое можно было изменять независимо.

### Мотивация

> Рассмотрим иерархию графических примитивов, которые надо реализовать на различных графических средах (GDI, .Net, DirectX).  
> Идея состоит в том, что примитивы содержат геометрические свойства, а для рисования используют специальный класс – рисовальщик.  
> Например, для иерархии графических примитивов реализацией может обеспечивать вывод точки на дисплей.  
> Абстракцией – класс точки, а её уточнениями – классы линии, прямоугольника, круга и т.д.  
> Все они используют исходную реализацию для рисования и для того, чтобы перенести их на новую платформу достаточно заменить реализацию.

### Используется если нужно:

1. Независимо изменять интерфейс работы с клиентом и реализацию;
2. Выбирать реализацию в процессе работы программы;
3. Использовать одну реализацию в нескольких абстракциях;
4. Уменьшить число классов, получающихся при использовании наследования;

### Структура

- <b>Абстракция (Abstraction)</b> – определяет базовый интерфейс для работы с клиентом.
- <b>Уточненная абстракция (Refined abstraction)</b> – наследует абстракцию и вносит дополнительные свойства и методы.
- <b>Реализация (Implementor)</b> – определяет интерфейс реализаций.
- <b>Конкретная реализация (Concrete implementor)</b> – обеспечивает определенную функциональность.

![](./resources/bridge/struct.png)

### Реализация

> Очень важным моментом в проектировании Моста является разработка двух интерфейсов: абстракции и её взаимодействия с реализацией.  
> Чем меньше будет в них привязка к конкретной реализации, тем проще будет заменить её в дальнейшем.  
> Например, использование для задания координаты класса Point из .NET усложнит последующий перенос на WinAPI.  
> При порождении экземпляра объекта, выбор конкретной реализации можно переложить на порождающие шаблоны [Фабричный метод](https://github.com/Grezer/patterns#factory-method-virtual-constructor) или [Абстрактная фабрика](https://github.com/Grezer/patterns#abstract-factory-kit).  
> Кроме того, они же могут применяться для определения нужной клиенту уточненной абстракции.

### Разультаты

- (+) Проще расширять систему новыми типами за счет сокращения общего числа родственных подклассов.

- (+) Возможность динамического изменения реализации в процессе выполнения программы.

- (+) Паттерн Bridge полностью скрывает реализацию от клиента. В случае модификации реализации пользовательский код не требует изменений.

- (+) Обе стороны – и абстракция и реализация – могут изменяться независимо.

---

## Flyweight

### [Refactoring guru](https://refactoring.guru/ru/design-patterns/flyweight)

### Назначение

> Паттерн Flyweight использует разделение для эффективной поддержки большого числа мелких объектов.

### Мотивация

> Проектирование системы из объектов самого низкого уровня обеспечивает оптимальную гибкость, но может быть неприемлемо "дорогим" решением с точки зрения производительности и расхода памяти.  
> Паттерн Flyweight описывает, как совместно разделять очень мелкие объекты без чрезмерно высоких издержек.  
> Каждый объект-приспособленец имеет две части: внутреннее и внешнее состояния.  
> Внутреннее состояние хранится (разделяется) в приспособленце и состоит из информации, не зависящей от его контекста.  
> Внешнее состояние хранится или вычисляется объектами-клиентами и передается приспособленцу при вызове его методов.

### Используется если нужно:

- Нужна поддержка огромного кол-ва однотипных объектов (например частицы в играх)

### Структура

Классы, описывающие различных насекомых Ant (муравей), Locust (саранча) и Cockroach (таракан) могут быть "легковесными", потому что специфичная для экземпляров информация может быть вынесена наружу и затем, передаваться клиентом в запросе.  
Класс Factory необходим для создания новых экземпляров или получения ссылки на уже существующий экземпляр

![](./resources/flyweight/struct.png)

![](./resources/flyweight/struct2.png)

### Реализация

- Разделите состояние целевого класса на разделяемое (внутреннее) и неразделяемое (внешнее).
- Удалите из атрибутов (членов данных) класса неразделяемое состояние и добавьте его в список аргументов, передаваемых методам.
- Создайте фабрику, которая может кэшировать и повторно использовать существующие экземпляры класса.
- Для создания новых объектов клиент использует эту фабрику вместо оператора new.
- Клиент (или третья сторона) должен находить или вычислять неразделяемое состояние и передавать его методам класса.

<b>Основная проблема</b> – разделение класса.

### Разультаты

- (+) Эффективная работа с большим числом мелких объектов.

- (-) Необходимость ведения базы данных Flyweight в FlyweightFactory, т.е. отслеживание уже не используемых Flyweight.

---

## Chain of Responsibility

### [Refactoring guru](https://refactoring.guru/ru/design-patterns/chain-of-responsibility)

### Назначение

> Позволяет избежать привязки отправителя запроса к конкретному получателю, давая возможность обработать запрос нескольким объектам.  
> Связывает объекты – получатели в цепочку и передает запрос вдоль этой цепочки, пока его не обработают.

### Мотивация

> Рассмотрим типичное Windows приложение.  
> В любой момент пользователь может нажать клавишу F1, ожидая получить подсказку.  
> Подсказка (Help), может быть как обычной – первая страница – оглавление, так и контекстно зависимой – т.е. высвечивается страница, соответствующее активному в данный момент окну приложения.  
> Обычно это достигается вызовом службы Help с идентификатором текущей страницы:  
> `Help.Show(helpCtx);` где <i>helpCtx</i> – индекс текущего окна.  
> Ситуация усложняется если программа допускает расширения в виде плагинов (plugin).  
> Новая функциональность, добавляемая с помощью плагина, может содержать как дополнительные диалоговые окна, так и дополнительную пользовательскую документацию (help).  
> <b>Вопрос</b> – как интегрировать дополнительные страницы подсказок?

### Структура

![](./resources/chain_of_responsibility/struct.png)

![](./resources/chain_of_responsibility/struct2.png)

### Реализация

1. Гарантия обработки запроса.  
   Последний обработчик в цепочке должен гарантировано обрабатывать любой запрос.  
   Например, в случае подсказки – выдавать начальную страницу документации, оглавление.  
   В случае функционального запроса – вызвать исключительную ситуацию «Function is not imlemented».

2. Добавление нового обработчика.  
   Как правило, его ставят в начало списка, чтобы гарантировать, что именно он будет обрабатывать запросы, относящиеся к его компетенции.

3. Обработчик может сделать часть работы, передав остаток работы следующему.

4. Порядок обработчиков в списке может влиять на результат, если один и тот же запрос могут обработать несколько обработчиков.  
   Тогда такой запрос будет обработан первым из них.  
   Это следует иметь ввиду при реализации паттерна.

### Пример - Автоматическая банковская машина.

Сначала пытаемся набрать необходимую сумму крпными купюрами (обработчик №1).  
Если этого неудалось сделать, пробуем добавить купюрами поменьше (обработчик №2) и т.д.

![](./resources/chain_of_responsibility/example.png)

### Разультаты

- (+) <b>Ослабление связности.</b>  
  Шаблон освобождает клиента от необходимости «знать» конкретного обработчика его запроса.  
  Отправителю и получателю ничего не известно друг о друге.

- (+) <b>Дополнительная гибкость</b> при разделении обязанностей между объектами.  
  Изменить обработку запроса можно либо путем добавления нового обработчика, либо перестановками обработчиков в цепочке.

- (-) Обработка запроса <b>не гарантирована</b>.

---

## Command (Action / Transaction)

### [Refactoring guru](https://refactoring.guru/ru/design-patterns/command)

### Назначение

> Инкапсулирует действие, как объект, позволяя тем самым задавать параметры обработки запросов, ставить эти действия в очередь, поддерживает протоколирование и отмену результатов действия.

### Мотивация

> <b>Заряженный курсор</b>  
> Во многих программах используется механизм «заряженный курсор».  
> Например, пользователь выбирает действие «удалить объект» и, далее, с помощью курсора выбирает объект, который будет удален.  
> Здесь важно, что момент определения действия отделен от момента его выполнения.  
> В нашем примере векторного графического реждактора можно определить функцию «закрасить фигуру выбранным цветом», так что пользователь сначало выбирает цвет кисти, а затем последовательно закрашивает фигуры на панели, выбирая их курсором.  
> Можно и создание новых фигур реализовать с помощью этого шаблона.  
> Тогда, выбрав желаемую фигуру из палитры, пользователь создает новые экземпляры кликая на панели.

> <b>Реализация «событий» в оконном приложении</b>  
> Действие, необходимое при нажатии на кнопку в окне, оформляется в виде команды, ассоциированной с этой кнопкой.  
> Аналогично, действие, необходимое при нажатии пункта меню, также оформляется в виде команды.

### Структура

![](./resources/command/struct.jpg)

### Реализация

С помощью этого шаблона можно реализовать команду UnDo, т.е. вернуть систему в состояние перед выполнением команды.  
Для этого необходимо добавить новый метод UnDo и поля, необходимые для восстановления состояния.

### Разультаты

- (+) Паттерн <i>Command</i> отделяет объект, инициирующий операцию, от объекта, который знает, как ее выполнить.  
  Единственное, что должен знать инициатор, это как отправить команду.

- (+) Дополнительная гибкость: позволяет осуществлять динамическую замену команд, использовать сложные составные команды, осуществлять отмену операций.

---

## Iterator (Cursor)

### [Refactoring guru](https://refactoring.guru/ru/design-patterns/iterator)

### Назначение

> Предоставляет способ последовательного доступа ко всем элементам составного объекта, не раскрывая его внутреннего представления.

### Мотивация

> Составной объект, такой как список, должен предоставлять способ доступа к его элементам без раскрытия своей внутренней структуры.  
> Более того, иногда нужно перебирать элементы списка различными способами, в зависимости от конкретной задачи.  
> Но вы, вероятно, не хотите раздувать интерфейс списка операциями для различных обходов, даже если они необходимы.  
> Кроме того, иногда нужно иметь несколько активных обходов одного списка одновременно.  
> Было бы хорошо иметь единый интерфейс для обхода разных типов составных объектов (т.е. полиморфная итерация).
> Паттерн Iterator позволяет все это делать.  
> <b>Ключевая идея</b> состоит в том, чтобы ответственность за доступ и обход переместить из составного объекта на объект <i>Iterator</i>, который будет определять стандартный протокол обхода.  
> ![](./resources/iterator/example.png)  
> Абстракция <i>Iterator</i> имеет основополагающее значение для технологии, называемой "обобщенное программирование".  
> Эта технология четко разделяет такие понятия как "алгоритм" и "структура данных".  
> <b>Мотивирующие факторы:</b> способствование компонентной разработке, повышение производительности и снижение расходов на управление.

### Пример

Если вы хотите одновременно поддерживать четыре вида структур данных (массив, бинарное дерево, связанный список и хэш-таблица) и три алгоритма (сортировка, поиск и слияние), то традиционный подход потребует 12 вариантов конфигураций (четыре раза по три), в то время как обобщенное программирование требует лишь 7 (четыре плюс три).

### Структура

![](./resources/iterator/struct.png)

### Реализация

1. Итератор хранит внутри себя текущую позицию.  
   Это позволяет клиенту иметь несколько активных итераторов к одной и той же коллекции.

2. Итераторы могут реализовывать разные алгоритмы обхода: обратный итератор, обход дерева в иерархическом порядке или слева-направо и т.п.

3. Если в процессе обхода коллекция была изменена – итератор может стать нелегальным.  
   Необходимы методы контроля применимости итератора.

### Разультаты

- (+) Упрощает интерфейс аггрегатора – не нужны дополнительные методы доступа.

- (-) Итераторы неустойчивы – необходим контроль за корректностью итератора.
