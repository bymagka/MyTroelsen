.assembly extern mscorlib
{
	.publickeytoken = (B7 7A 5C 56 19 34 E0 89)
	.ver 4:0:0:0
}

.assembly extern System.Windows.Forms
{
	.publickeytoken = (B7 7A 5C 56 19 34 E0 89)
	.ver 4:0:0:0
}

//Эта сборка
.assembly CILCars
{
	.hash algorithm 0x00008004
	.ver 1:0:0:0
}

.module CILCars.dll


.namespace CILCars
{
	.class public auto ansi beforefieldinit CILCar
		extends [mscorlib]System.Object
	{
			.field public string petName
			.field public int32 currSpeed
		
		//Специальный конструктор позволяет вызывающему коду присваивать поля данных
		.method public hidebysig specialname rtspecialname
		instance void .ctor(int32 c,string p) cil managed
		{
			.maxstack 8

			//Загрузить первый элемент в стек и вызвать конструктор базового класса
			ldarg.0 //объект this!!! а не int32 c
			call instance void [mscorlib]System.Object::.ctor()

			//Загрузить первый и второй аргументы в стек
			ldarg.0 //объект this
			ldarg.1 //int32 c

			//Сохранить самый верхний элемент стека (int32) в поле currSpeed
			stfld int32 CILCars.CILCar::currSpeed

			//Загрузить строковый элемент и сохранить в поле petName
			ldarg.0 //объект this
			ldarg.2 //объект string p
			stfld string CILCars.CILCar::petName
			ret
		}

	}



	.class public auto ansi beforefieldinit CILCarInfo
		extends [mscorlib]System.Object
		{
			.method public hidebysig static void
				Display(class CILCars.CILCar c) cil managed
				{
					.maxstack 8

					//Нужна локальная строковая переменная
					.locals init ([0] string caption)

					//Загрузить строку и входной параметр CILCar в стек
					ldstr "{0}'s speed is:"
					ldarg.0

					//Поместить в стек значение поля petName и вызвать статический метод Format 
					ldfld string CILCars.CILCar::petName
					call string [mscorlib]System.String::Format(string,object)
					stloc.0

					//Загрузить значение поля currSpeed и получить его строковое представление (тут есть вызов ToString)
					ldarg.0
					ldflda int32 CILCars.CILCar::currSpeed
					call instance string [mscorlib]System.Int32::ToString()
					ldloc.0

					//Вызвать метод MessageBox.Show() с загруженными значениями

					call valuetype [System.Windows.Forms]
						System.Windows.Forms.DialogResult
						[System.Windows.Forms]
						System.Windows.Forms.MessageBox::Show(string,string)

					pop
					ret	
				}
		}
}


		