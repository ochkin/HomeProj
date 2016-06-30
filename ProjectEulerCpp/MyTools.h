#ifdef MYTOOLS_EXPORTS
#define MYTOOLS_API __declspec(dllexport) 
#else
#define MYTOOLS_API __declspec(dllimport) 
#endif