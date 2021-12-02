.assembly extern mscorlib
{
	.publickeytoken = (B7 7A 5C 56 19 34 E0 89)
	.ver 4:0:0:0
}


.assembly CILTypes 
{
	.ver 1:0:0:0 
}

.module CILTypes.dll

.namespace MyCompany
{
	.class public interface IMyInterface {}

	.class public interface IMyOtherInterface
		implements MyCompany.IMyInterface {}

	.class public MyBaseClass 
	{
		.field private string stringField 
		.field private int32 intField 

		//конструктор
		.method public hidebysig specialname rtspecialname
			instance void .ctor(string s,int32 i) cil managed
			{
				ret
			}

		//свойство TheString
		.method public hidebysig specialname
			instance string get_TheString() cil managed
		{
			ret
		}

		.method public hidebysig specialname
			instance void set_TheString(string 'value') cil managed
		{
			ret
		}

		.property instance string TheString()
		{
			.get instance string
				MyCompany.MyBaseClass::get_TheString()
			.set instance void
				MyCompany.MyBaseClass::set_TheString(string)
		}

		//пример передачи параметров по ссылке (&) , по значению, или пользовательский класс + выходной параметр
		.method public hidebysig static void MyMethod(int32 inputInt, int32& refint, class [mscorlib]System.Collections.ArrayList ar, [out] int32& outputInt) cil managed
		{

		}

		//пример реализации метода с объявлением локальных переменных
		.method public hidebysig static void MyLocalVariables() cil managed
		{
			//максимально возможное количество переменных в стеке
			.maxstack 8

			//Определить три локальные переменные
			.locals init([0] string myStr, [1] int32 myInt,[2] object myObj)

			//Загрузить строку в виртуальный стек выполнения
			ldstr "CIL code is fun"

			//Извлечь текущее значение и сохранить его в локальной переменной [0]
			stloc.0

			//Загрузить константу типа i4(сокращение для int32) со значением 33
			ldc.i4 33

			//Извлечь текущее значение в стеке и сохранить его в переменную 1.
			stloc.1

			//Создать новый объект и поместить его в стек
			newobj instance void [mscorlib]System.Object::.ctor()

			//Извлечь текущее значение из стека и сохранить его в переменную 2
			stloc.2

			//возврат
			ret
		}

		//пример реализации  статического метода суммирования двух переменных
		.method public hidebysig static int32 Add(int32 a,int32 b) cil managed
		{
		 .maxstack 2

		 //загрузить 0 аргумент метода в стек
		 ldarg.0

		//загрузить 1 аргумент метода в стек
		 ldarg.1

		 //просуммировать 2 верхних значения стека
		 add

		 //вернуть верхний элемент стека и выйти из метода
		 ret

		}

		//пример реализации метода с циклом 
		//Метки (IL_) сделаны для того, чтобы была возможность указать точку кода, куда надо осуществить переход при ветвлении
		//public static void CountToTen()
		//{
		//for(int i = 0; i < 10; i++)
		//	{
		//
		//	}
		//}
		//
		//
		.method public hidebysig static void CountToTen() cil managed
		{
			.maxstack 8

			//Инициализировать локальную целочисленную переменную 1
			.locals init ([0] int32 i)

			//Загрузить это значение в стек
			IL_0000: ldc.i4.0

			//Сохранить это значение в переменную 0
			IL_0001: stloc.0

			//Перейти на метку IL_0008
			IL_0002: br.s IL_0008

			//Загрузить значение переменной по индексу 0
			IL_0004: ldloc.0

			//Загрузить значение 1 в стек
			IL_0005: ldc.i4.1

			//Добавить текущее значение в стеке по индексу 0
			IL_0006: add
			IL_0007: stloc.0

			//Загрузить значение по индексу 0
			IL_0008: ldloc.0

			//Загрузить значение 10 в стек
			IL_0009: ldc.i4.s 10

			//Меньше чем? Если да, то перейти на метку IL_0004
			IL_000b: blt.s IL_0004

			IL_000d: ret
		}
	}

	.class public MyDerivedClass 
		extends MyCompany.MyBaseClass 
		implements MyCompany.IMyInterface {}

	//structure. value = extends [mscorlib]System.ValueType
	.class public sealed value MyStruct{}

	//enum
	.class public sealed enum MyEnum 
	{
		.field public static literal valuetype
		MyCompany.MyEnum A = int32(0)

		.field public static literal valuetype
		MyCompany.MyEnum B = int32(1)

		.field public static literal valuetype
		MyCompany.MyEnum C = int32(2)
	}

}