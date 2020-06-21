// prolog
.assembly extern mscorlib { }
.assembly calculator { }
.method static void main()
{
.entrypoint
.try
{

// variables
.locals init ( int32 i )
.locals init ( int32 z )
.locals init ( float64 d )
.locals init ( bool t )
.locals init ( bool f )

// main
ldc.i4 5
stloc i
ldc.r8 123.456
stloc d
ldc.i4 1
stloc t
ldc.i4 0
stloc f
ldc.i4 34
stloc z
ldloc i
call void [mscorlib]System.Console::Write(int32)
ldstr "\n"
call void [mscorlib]System.Console::Write(string)
ldloc d
call void [mscorlib]System.Console::Write(float64)
ldstr "\n"
call void [mscorlib]System.Console::Write(string)
ldloc t
call void [mscorlib]System.Console::Write(bool)
ldstr "\n"
call void [mscorlib]System.Console::Write(string)
ldloc f
call void [mscorlib]System.Console::Write(bool)
ldstr "\n"
call void [mscorlib]System.Console::Write(string)
ldc.i4 7
stloc i
ldloc i
call void [mscorlib]System.Console::Write(int32)
ldstr "\n"
call void [mscorlib]System.Console::Write(string)
ldc.i4 15
stloc i
ldloc i
ldloc z
add
call void [mscorlib]System.Console::Write(int32)
ldstr "\n"
call void [mscorlib]System.Console::Write(string)
ldc.i4 9
stloc i
ldloc i
call void [mscorlib]System.Console::Write(int32)
ldstr "\n"
call void [mscorlib]System.Console::Write(string)
ldc.i4 1
brfalse L0
ldloc i
call void [mscorlib]System.Console::Write(int32)
ldstr "\n"
call void [mscorlib]System.Console::Write(string)
ldloc i
ldc.i4 5
add
call void [mscorlib]System.Console::Write(int32)
ldstr "\n"
call void [mscorlib]System.Console::Write(string)
L0:
L2:
ldc.i4 0
brfalse L1
ldloc i
ldc.i4 1
add
stloc i
br L2
L1:
ldloc i
ldc.i4 5
add
call void [mscorlib]System.Console::Write(int32)
ldstr "\n"
call void [mscorlib]System.Console::Write(string)
// epilog
leave EndMain
}
catch [mscorlib]System.Exception
{
callvirt instance string [mscorlib]System.Exception::get_Message()
call void [mscorlib]System.Console::WriteLine(string)
leave EndMain
}
EndMain: ret
}

