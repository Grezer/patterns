# Паттерны

## Порождающие

### Создание объектов. Нужны, что бы объекты точно удалялись (не оставались как балласт) и не удалялись раньше времени

1. [Singleton](https://github.com/Grezer/patterns#singleton)
2. Factory method
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

---

## Singleton

Создание класса без публичных констркторов.  
Применяется когда нужен класс, который:

- гарантированно имел бы <b>ОДИН</b> экземпляр
- этот экземпляр должен быть доступен в любой точке приложения

Пример: Window manager

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

<p align="center">
  <img width="471" height="400" src="./resources/singleton/struct.png">
</p>
