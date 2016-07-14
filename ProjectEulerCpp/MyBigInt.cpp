#include "MyBigInt.h"
#include <string>
#include <sstream>

MyBigInt::MyBigInt()
{
	this->data.push_back(0);
}

MyBigInt::MyBigInt(unsigned long int original)
{
	this->data.push_back(original % MyBigInt::MAX1);
	if (MyBigInt::MAX < original)
		this->data.push_back(original / MyBigInt::MAX1);
}

MyBigInt::~MyBigInt()
{
}

void MyBigInt::Add(unsigned long int value)
{
	unsigned short int offset = 0;
	unsigned long int fraction, current;
	while (0 < value)
	{
		fraction = value % MAX1;
		value = value / MAX1;
		//current = offset < this->data.size() ? this->data.at(offset) : 0;
		if (offset < this->data.size())
		{
			current = this->data.at(offset);
		}
		else
		{
			current = 0;
			this->data.push_back(0);
		}

		unsigned long int diff = MyBigInt::MAX1 - current;
		if (fraction < diff)
		{
			this->data[offset] = current + fraction;
		}
		else
		{
			this->data[offset] = fraction - diff;
			value++;
		}

		offset++;
	}
}

void MyBigInt::Add(const MyBigInt & value)
{
	unsigned long int inMind = 0;
	for (unsigned short int offset = 0;
		offset < this->data.size() || offset < value.data.size() || 0 < inMind;
		offset++)
	{
		unsigned long int current = inMind;
		if (offset < this->data.size())
			current += this->data.at(offset);
		if (offset < value.data.size())
			current += value.data.at(offset);

		if (this->data.size() <= offset)
			this->data.push_back(0);
		this->data.at(offset) = current % MAX1;
		inMind = current / MAX1;
	}
}

void MyBigInt::divideMulSum(unsigned long int a, unsigned long int b, unsigned long int &inMind, unsigned long int &current)
{
	unsigned long int remA = a % MAX1, remB = b % MAX1;
	unsigned long int remAB = 0,
		divAB = (a / MAX1) * (b / MAX1) * MAX1 + (a / MAX1) * remB + remA * (b / MAX1),
		temp;
	for (unsigned int i = 0; i < remB; i++)
	{
		temp = MAX1 - remAB;
		if (remA < temp)
		{
			remAB += remA;
		}
		else
		{
			remAB = remA - temp;
			divAB++;
		}
	}

	//this->data.at(offset) = (this->data.at(offset) * multiplier + inMind) % MAX1;
	unsigned long int tempInMind = divAB + (inMind / MAX1) + (remAB + (inMind % MAX1)) / MAX1;
	current = (remAB + (inMind % MAX1)) % MAX1;
	inMind = tempInMind;
}

void MyBigInt::Multiply(unsigned long int multiplier)
{
	unsigned short int offset = 0;
	unsigned long int current = 0, inMind = 0;
	while (offset < this->data.size() || inMind > 0)
	{
		divideMulSum(
			offset < this->data.size() ? this->data.at(offset) : 0,
			multiplier,
			inMind,
			current);
		if (offset < this->data.size())
		{
			this->data.at(offset) = current;
		}
		else
		{
			this->data.push_back(current);
		}

		offset++;
	}
}

string PadLeft(std::string source, int length)
{
	if (source.length() < length)
	{
		stringstream ss;
		for (int i=0; i < length - source.length(); i++)
			ss << "0";
		ss << source;
		return ss.str();
	}
	else
	{
		return source;
	}
}

string MyBigInt::ToString()
{
	string result("");
	//for (auto i = this->data.begin(); i != this->data.end(); i++)
	for (int i=0; i < this->data.size(); i++)
		result = to_string(this->data.at(i)) + PadLeft( result, 9 * i);
	return result;
}

unsigned long int MyBigInt::SumDigits()
{
	unsigned long int sum = 0;
	//for (auto i = this->data.begin(); i != this->data.end(); i++)
	for (auto& value : this->data)
	{
		//unsigned long int value = *i;
		while (0 < value )
		{
			sum += value % 10;
			value = value / 10;
		}
	}
	return sum;
}

unsigned int MyBigInt::NumDigits()
{
	unsigned long int last = this->data.back();
	unsigned int numDigits;
	if (99999999 < last)
	{
		numDigits = 9;
	}
	else
	{
		if (9999999 < last)
		{
			numDigits = 8;
		}
		else
		{
			if (999999 < last)
			{
				numDigits = 7;
			}
			else
			{
				if (99999 < last)
				{
					numDigits = 6;
				}
				else
				{
					if (9999 < last)
					{
						numDigits = 5;
					}
					else
					{
						if (999 < last)
						{
							numDigits = 4;
						}
						else
						{
							if (99 < last)
							{
								numDigits = 3;
							}
							else
							{
								if (9 < last)
								{
									numDigits = 2;
								}
								else
								{
									numDigits = 0 < last ? 1 : 0;
								}
							}
						}
					}
				}
			}
		}
	}
	return 9 * (this->data.size() - 1) + numDigits;
}
