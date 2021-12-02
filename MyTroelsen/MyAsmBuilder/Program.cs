using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;

namespace MyAsmBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Dynamic Assembly");

            AppDomain currDomain = Thread.GetDomain();

            CreateAsm(currDomain);

            Console.WriteLine("Finish creating assembly");

            //Загрузим сборку из файла
            Console.WriteLine("Loading assembly");

            Assembly assembly = Assembly.Load("MyAssembly");

            //Получим тип
            Type hello = assembly.GetType("MyAssembly.HelloWorld");

            //создадим объект hello
            Console.WriteLine("Passing hello world class");

            string msg = Console.ReadLine();

            object[] helloArguments = new object[1];

            helloArguments[0] = msg;

            object helloInstance = Activator.CreateInstance(hello, helloArguments);

            //получим поле theMessage и выведем
            FieldInfo field = hello.GetField("theMessage", BindingFlags.Instance | BindingFlags.NonPublic);
            Console.WriteLine(field.GetValue(helloInstance));

            //Вызовем Say hello
            MethodInfo mi = hello.GetMethod("SayHello");
            mi.Invoke(helloInstance, null);

            Console.ReadLine();
        }

        public static void CreateAsm(AppDomain currAppDomain)
        {

            //общие характеристики сборки
            AssemblyName assemblyName = new AssemblyName();//new AssemblyName("MyAssemblyName")
            assemblyName.Name = "MyAssembly";
            assemblyName.Version = new Version("1.0.0.0");

            //создать новую сборку внутри текущего домена приложения
            AssemblyBuilder assembly = currAppDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Save);

            //Поскольку сборка однофайловая, то имя модуля будет таким же, как и у сборки
            ModuleBuilder module = assembly.DefineDynamicModule("MyAssembly","MyAssembly.dll");

            //Определить закрытую переменную-член типа String по имени HelloWorld.
            TypeBuilder HelloWorldClass = module.DefineType("MyAssembly.HelloWorld",TypeAttributes.Public);

            //Определить закрытую переменную - член класса
            FieldBuilder msgField = HelloWorldClass.DefineField("theMessage", Type.GetType("System.String"), FieldAttributes.Private);

            //Создать специальный конструктор
            Type[] constructorArgs = new Type[1];

            constructorArgs[0] = typeof(String);

            ConstructorBuilder constructor = HelloWorldClass.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, constructorArgs);

            ILGenerator constructorIL = constructor.GetILGenerator();

            constructorIL.Emit(OpCodes.Ldarg_0);
            Type objectClass = typeof(object);
            ConstructorInfo constructorInfo = objectClass.GetConstructor(new Type[0]);
            constructorIL.Emit(OpCodes.Call,constructorInfo);
            constructorIL.Emit(OpCodes.Ldarg_0);
            constructorIL.Emit(OpCodes.Ldarg_1);
            constructorIL.Emit(OpCodes.Stfld, msgField);
            constructorIL.Emit(OpCodes.Ret);

            //Создать стандартный конструктор
            HelloWorldClass.DefineDefaultConstructor(MethodAttributes.Public);

            //Создать метод msg
            MethodBuilder methodMsg = HelloWorldClass.DefineMethod("GetMessage", MethodAttributes.Public,typeof(string),null);
            ILGenerator methodGenerator = methodMsg.GetILGenerator();
            methodGenerator.Emit(OpCodes.Ldarg_0);
            methodGenerator.Emit(OpCodes.Ldfld, msgField);
            methodGenerator.Emit(OpCodes.Ret);

            //Создать метод SayHello()
            MethodBuilder methodSayHello = HelloWorldClass.DefineMethod("SayHello", MethodAttributes.Public, null, null);
            ILGenerator sayHiGenerator = methodSayHello.GetILGenerator();
            sayHiGenerator.EmitWriteLine("Hello from say hello method");
            sayHiGenerator.Emit(OpCodes.Ret);

            //Выпустить класс HelloWorld
            HelloWorldClass.CreateType();

            //Сохранение динамической сборки в файл
            assembly.Save("MyAssembly.dll");


        }
    }
}
