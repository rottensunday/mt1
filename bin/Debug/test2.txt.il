// prolog
.assembly extern mscorlib { }
.assembly calculator { }
.method static void main()
{
.entrypoint
.try
{

// variables
.locals init ( int32 'a' )
.locals init ( float64 'b' )
.locals init ( bool 'c' )

call class [mscorlib]System.Threading.Thread [mscorlib]System.Threading.Thread::get_CurrentThread()
call class [mscorlib]System.Globalization.CultureInfo [mscorlib]System.Globalization.CultureInfo::get_InvariantCulture()
callvirt instance void [mscorlib]System.Threading.Thread::set_CurrentCulture(class [mscorlib]System.Globalization.CultureInfo)
// main
call string [mscorlib]System.Console::ReadLine()
call int32 [mscorlib]System.Int32::Parse(string)
stloc 'a'
ldloc 'a'
ldc.i4 5
add
call void [mscorlib]System.Console::Write(int32)
call string [mscorlib]System.Console::ReadLine()
call float64 [mscorlib]System.Double::Parse(string)
stloc 'b'
ldstr "\n"
call void [mscorlib]System.Console::Write(string)
call class [mscorlib]System.Globalization.CultureInfo[mscorlib] System.Globalization.CultureInfo::get_InvariantCulture()
ldstr "{0:0.000000}"
ldloc 'b'
ldc.r8 12.3
add
box [mscorlib]System.Double
call string[mscorlib] System.String::Format(class [mscorlib] System.IFormatProvider, string, object)
call void [mscorlib]System.Console::Write(string)
ldstr "\n"
call void [mscorlib]System.Console::Write(string)
call string [mscorlib]System.Console::ReadLine()
call bool [mscorlib]System.Boolean::Parse(string)
stloc 'c'
ldloc 'c'
call void [mscorlib]System.Console::Write(bool)
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

