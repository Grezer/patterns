# Паттерны

## Порождающие

### Создание объектов. Нужны, что бы объекты точно удалялись (не оставались как балласт) и не удалялись раньше времени

1. [Singleton](https://github.com/Grezer/patterns#singleton)
2. [Factory method](https://github.com/Grezer/patterns#factory-method-virtual-constructor)
3. Prototype
4. Abstract factory
5. Builder

## Структурные

### Создание сложных объектов из простых, не используя наследование

1. Adapter
2. Decorator
3. Composite
4. Proxy
5. Facade
6. Bridge
7. Flyweight

## Поведенческие

### Изменение поведения объектов в run time (объект должен по разному себя вести в разные моменты времени)

1. Chain of Responsibility
2. Command
3. Iterator
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

<p align="center">
  <img width="1100" height="380" src="./resources/factory_method/struct.png">
</p>

### Участники

<b>Product</b> – базовый класс для семейства конкретных продуктов, экземпляры которых должны инстанцироваться.  
<b>Creator</b> – базовый класс для семейства «создателей», классы реализующие метод, создающий новый экземпляр соответствующего класса.  
Для каждого класса ConcreteProduct должен быть соответствующий класс ConcreteCreator, задача которого изготавливать экземпляры класса ConcreteProduct.

### Разультаты

- (+) Снижает зависимость между классами. В нашем примере редактор работает только с экземплярами класса Figure, экземпляры конкретных классов создаются с помощью «создателя».
- (+) Решает задачу "разрывающую" два разных события:
  1. Выбор "кого мы делаем" (прямоугольник/треугольник/элипс)
  2. Изготовление этого экземпляра
