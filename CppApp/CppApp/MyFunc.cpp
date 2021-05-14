#define MyFunc _declspec(dllexport)

struct MyStruct
{
	int a, b;
};

extern "C" {
	MyFunc int AddNumbers(int a, int b) {
		return a + b;
	};
	MyFunc int SubtractNumbers(int a, int b)
	{
		return a - b;
	};

	MyFunc int AddStruct(MyStruct vals) {
		return vals.a + vals.b;
	};
	MyFunc int SubtractStruct(MyStruct vals)
	{
		return vals.a - vals.b;
	};
}