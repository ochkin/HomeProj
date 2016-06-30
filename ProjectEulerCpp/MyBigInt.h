#pragma once
#include "MyTools.h"
#include <vector>

using namespace std;

class __declspec( dllexport ) MyBigInt
{
private:
	static const  unsigned long int MAX = 999999999ul;
	static const  unsigned long int MAX1 = 1000000000ul; // 1 bln
	// ULONG_MAX ~ 4 bln
	vector<unsigned long int> data;
	static void divideMulSum(
		unsigned long int a, unsigned long int b, unsigned long int &inMind, unsigned long int &current);
public:
	MyBigInt(void);
	MyBigInt(unsigned long int);
	~MyBigInt(void);
	void Add(unsigned long int);
	void Add(const MyBigInt&);
	void Multiply(unsigned long int);
	 string ToString();
	unsigned long int SumDigits();
	unsigned int NumDigits();
};

