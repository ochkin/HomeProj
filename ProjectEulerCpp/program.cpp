//#include "..\MAPM\M_APM.H"
#include <vector>
#include <iostream>
#include <fstream>
#include <math.h>
#include <numeric>
#include <utility>
#include "MyBigInt.h"
#include <set>
#include <unordered_set>
#include <map>
#include <unordered_map>
#include <string>
#include <algorithm>
#include <bitset>
#include <ctime>
#include <windows.h>

using namespace std;

int problem16_old(int a, int b)
{
	int n = floor(b * log10(a))  +1;

	long double y = exp(1000 * logl(2) - n * logl(10));
	
	//int* x = new int [n];
	int sum = 0;
	//delete [] x;

	int temp;
	for (int i = 0; i < n; i++)
	{
		/*a[i] = ;
		long double z = y;
		for (int j = 0; j <= i; j++)
			z *= 10;
		temp = z;
		*/

		y *= 10;
		temp = y;
		y -= temp;
		sum += temp;
	}

	//return exp(b * log(a) - n * log(10));
	return sum; //196
}

int problem16(int a, int b)
{
	vector<unsigned short int> x, y;
	// x = a ^ 0
	x.push_back(1);

	for (unsigned int power = 0; power < b; power++)
	{
		// y = x * a
		y.clear();

		//for (auto digit = x.begin(); digit != x.end(); digit++)
		for (unsigned int digit = 0; digit < x.size(); digit++)
		{
			// y += x[digit] * a
			unsigned int toAdd = x[digit] * a;
			unsigned short int toAddDigit;
			unsigned short int inMemory = 0;
			unsigned short int offset = 0;
			while (0 < toAdd || 0 < inMemory)
			{
				toAddDigit = (toAdd % 10) + (inMemory % 10);
				toAdd = toAdd / 10;
				inMemory = inMemory / 10;
				
				if (digit + offset < y.size())
					toAddDigit += y[digit + offset];
				inMemory += toAddDigit / 10;
				toAddDigit = toAddDigit % 10;
				if (digit + offset < y.size())
				{
					y[digit + offset] = toAddDigit;
				}
				else
				{
					while (y.size() < digit + offset)
						y.push_back(0);
					y.push_back(toAddDigit);
				}

				offset++;
			}
		}

		// swap x & y
		swap(x, y);
	}
	
	//return x.size(); // 302
	return accumulate(x.begin(), x.end(), 0);
}

string digit[10] = {"", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
string teen[10] = {"ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen"};
string ten[10] = {"", "", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety"};

int numDigit(int value)
{
	if (value < 0 || 9 < value)
		throw new exception("digit out of range.");

	return digit[value].length();
}

int numLetters2d(int value2d)
{
	if (value2d < 1 || 99 < value2d)
		throw new exception("value2d out of range.");

	if (1 <= value2d && value2d <= 9)
		return numDigit(value2d);
	if (10 <= value2d && value2d <= 19)
		return teen[value2d - 10].length();
	if (20 <= value2d && value2d <= 99)
	{
		return ten[value2d / 10].length() + digit[value2d % 10].length();
	}
	else
	{
		throw new exception("Error.");
	}
}

int problem17(/*int from, int to*/)
{
	int oneTo100 = 0;
	for (int i = 1; i <= 99; i++)
		oneTo100 += numLetters2d(i);
	//return oneTo100;

	int one1000 = 11; // "onethousand";
	int hun = 7; // "hundred"
	int huns = 9 * hun;
	for (int i = 1; i <= 9; i++)
		huns += digit[i].length();

	return 10 * oneTo100
		+ huns * 100
		+ 3 * 99 * 9 // and
		+ one1000; // 21124
}

vector<int*> read18(string fileName)
{
	ifstream input(fileName);
	if (input.is_open())
	{
		vector<int*> result;
		int value, i = 0;
		while (input >> value)
		{
			if (result.size() <= i)
			{
				result.push_back(new int[i+1]);
				i=0;
			}
			result.back()[i++] = value;
		}
		input.close();
		return result;
	}
	else
	{
		throw new exception("Unable to open file.");
	}
}

int problem18()
{
	auto input = read18("Problem18.txt");
	for (int level = input.size() - 2; 0 <= level; level--)
		for (int item = 0; item <= level; item++)
			input.at(level)[item] += max(input.at(level+1)[item], input.at(level+1)[item+1]);

	return input.front()[0]; // 1074
}

bool isLeapYear(unsigned short int year)
{
	if (year < 1000 || 2500 < year)
		throw new exception("year out of range.");
	return (year % 4 == 0 && year % 100 != 0) || year % 400 == 0;
}

unsigned short int daysInMonth(unsigned short int month, unsigned short int year)
{
	//if (month < 1 || 12 < month)
	//	throw new exception("month out of range.");
	switch (month)
	{
	case 1:
		return 31;
	case 2:
		return isLeapYear(year) ? 29 : 28;
	case 3:
		return 31;
	case 4:
		return 30;
	case 5:
		return 31;
	case 6:
		return 30;
	case 7:
		return 31;
	case 8:
		return 31;
	case 9:
		return 30;
	case 10:
		return 31;
	case 11:
		return 30;
	case 12:
		return 31;
	default:
		throw new exception("month out of range.");
	}
}

unsigned int problem19()
{
	unsigned int year = 1900, month = 1, dayOfWeek = 1, sundays = 0;
	while (year < 2001)
	{
		if (dayOfWeek == 0 && 1900 < year)
			sundays++;

		dayOfWeek = (dayOfWeek + daysInMonth(month, year)) % 7;
		if (month < 12)
		{
			month++;
		}
		else
		{
			month = 1;
			year++;
		}
	}
	return sundays; // 171
}

unsigned int problem20()
{
	unsigned int n = 100;
	MyBigInt x(2);
	for (unsigned int i=3; i<=n; i++)
		x.Multiply(i);
	return x.SumDigits(); // 648
}

int getSumDiv(int value)
{
	int sum = 1;
	for (int i=2; i<=sqrt(value); i++)
		if (value % i == 0)
		{
			sum += i;
			if (value / i != i)
				sum += value / i;
		}
	return sum;
}

int getAmicable(int value)
{
	int sumDiv = getSumDiv(value);
	int sumDiv2 = getSumDiv(sumDiv);
	return sumDiv2 == value ? sumDiv : -1;
}

unsigned int problem21()
{
	int N = 10000;
	set<int> amicables;
	int pair;
	for (int i=5; i<=N; i++)
		//pair = getAmicable(i);
		//if (0 < pair)
		//	if (i == getAmicable
		if (amicables.find(i) == amicables.end())
		{
			pair = getSumDiv(i);
			if (pair != i && i == getSumDiv(pair))
			{
				amicables.emplace(i);
				amicables.emplace(pair);
			}
		}
	//int sum = 0;
	//for (auto item = amicables.begin(); item != amicables.end(); item++)
	//	sum += *item;
	//return sum; // 40284
	return accumulate(amicables.begin(), amicables.end(), 0); //31626
}

unsigned long int problem22()
{
	const string LETTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
	unordered_map<char, unsigned short int> abc;
	for (unsigned short int i = 0; i < LETTERS.length(); i++)
		abc.emplace(LETTERS.at(i), i+1);

	ifstream input("Problem22.txt");
	if (input.is_open())
	{
		//vector<int*> result;
		//int value, i = 0;
		//while (input >> value)
		//{
		//	if (result.size() <= i)
		//	{
		//		result.push_back(new int[i+1]);
		//		i=0;
		//	}
		//	result.back()[i++] = value;
		//}
		char buffer[256];
		bool stop = false;
		int numSymbols;
		multiset<string> names;
		while (!stop)
		{
			input.getline(buffer, 256, ',');
			numSymbols = input.gcount();
			if (numSymbols == 0)
			{
				stop = true;
			}
			else
			{
				int lastComma;
				if (buffer[numSymbols-2] == '"')
					lastComma = numSymbols - 2;
				else
					lastComma = buffer[numSymbols-1] == '"' ? numSymbols - 1 : -1;

				if (numSymbols < 3 || buffer[0] != '"' || lastComma == -1)
				{
					string data(buffer, 0, numSymbols);
					string message = "Invalid data read." + data;
					throw new exception(message.c_str());
				}
				{
					std::string buffer2(buffer, 1, lastComma - 1);
					//cout << buffer2 << endl;
					//cin.get();
					names.emplace(buffer2);
				}
			}
		}
		input.close();
		cout << names.size() << " names have been read." << endl;

		unsigned long int sum = 0, position = 0, alphabeticalValue;
		for (auto name = names.begin(); name != names.end(); name++)
		{
			alphabeticalValue = 0;
			for (auto letter = (*name).begin(); letter != (*name).end(); letter++)
				alphabeticalValue += abc[*letter];
			sum += ++position * alphabeticalValue;
		}
		return sum; // 871198282
	}
	else
	{
		throw new exception("Unable to open file.");
	}
	//std::cout << std::string().max_size() << std::endl; // ~4 bln
}

bool isAbundant(unsigned int value)
{
	int sum = 1;
	bool exceeded = false;
	for (int i=2; i<=sqrt(value) && !exceeded; i++)
		if (value % i == 0)
		{
			sum += i;
			if (value / i != i)
				sum += value / i;
			exceeded = (value < sum);
		}
	return exceeded;
}

unsigned long int problem23()
{
	const unsigned int MAX = 28123;
	set<unsigned int> abundants;
	for (unsigned int i = 12; i < MAX; i++)
		if (isAbundant(i))
			abundants.emplace(i);
	cout << abundants.size() << " abundants found." << endl; // 6965

	set<unsigned int> sums;
	bool exceeded;
	for (auto first = abundants.begin(); first != abundants.end(); first++)
	{
		exceeded = false;
		for (auto second = abundants.begin(); second != abundants.end() && !exceeded; second++)
			if (*first + *second <= MAX)
			{
				sums.emplace(*first + *second);
			}
			else
			{
				exceeded = true;
			}
	}
	cout << sums.size() << " abundant sums found." << endl; // 26667

	unsigned int last = 1;
	unsigned long int result = 0;
	for (auto j = sums.begin(); j != sums.end(); j++)
	{
		for (unsigned int k = last; k < *j; k++)
			result += k;
		last = 1 + *j;
	}
	return result;  // 4179871
}

string problem24()
{
	//vector<char> chars {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};
	char zeroToNine[10] = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};
	vector<char> chars(zeroToNine, zeroToNine + 10);
	char permutation[10];
	
	unsigned int factorial = 1;
	for (unsigned int k = 1; k < 10; k++)
		factorial *= k;

	unsigned int n = 1000000 - 1;
	for (int i=1; i <= 9; i++)
	//while (1 < factorial)
	{
		unsigned int div = n / factorial;
		permutation[10 - chars.size()] = chars.at(div);
		chars.erase(chars.begin() + div);

		n = n % factorial;
		factorial = factorial / chars.size();
	}
	permutation[9] = chars.front();

	string result(permutation);
	return result; //  2783915604 // 2783915460
}

unsigned int problem25()
{
	MyBigInt a(89UL), b(144UL);
	unsigned int number = 12;
	MyBigInt *prev = &a, *next = &b;
	while (next->NumDigits() < 1000)
	{
		prev->Add(*next);
		swap(prev, next);

		number++;
		// DEBUG:
		//if ((next->SumDigits()) % 100 == 0)
		//	cout << number << "th number has " << next->SumDigits << " digits." << endl;
	}
	return number; //977 ///4782
}

int getCycle(int d)
{
	int left = 1;
	vector<int> history;
	while (0 < left && find(history.begin(), history.end(), left) == history.end())
	{
		history.push_back(left);
		//div = left / d;

		left = (left * 10) % d;
	}

	return left == 0 ? 0 : (history.end() - find(history.begin(), history.end(), left));
}

string problem26()
{
	int d = 7, cycle = 6;
	for (int n=11; n < 1000; n++)
	{
		int nCycle = getCycle(n);
		if (cycle < nCycle)
		{
			d = n;
			cycle = nCycle;
		}
	}
	return "1/" + to_string(d) + " has a recurring cycle with " + to_string(cycle) + " digits.";
	// 1/983 has a recurring cycle with 982 digits
}

bool isPrime(unsigned int value)
{
	if (value < 2 || value % 2==0)
		return false;

	for (unsigned int i = 3; i <= sqrt(value); i += 2)
		if (value % i == 0)
			return false;

	return true;
}

unsigned short int getNumQuadraticPrimes(int a, int b)
{
	unsigned short int n = 0;
	while (isPrime(n*n + a*n + b))
		n++;
	return n;
}

int problem27()
{
	int maxProduct = 0;
	unsigned short int maxNumPrimes = 0;
		for (int b = -999; b <= 999; b++)
			if (isPrime(b))
	for (int a = -999; a <= 999; a++)
		{
			int numQuadraticPrimes = getNumQuadraticPrimes(a, b);
			if (maxNumPrimes < numQuadraticPrimes)
			{
				maxProduct = a*b;
				maxNumPrimes = numQuadraticPrimes;
			}
		}
	return maxProduct; // -59231
}

unsigned long int problem28()
{
	unsigned short int n = 500;
	unsigned long int sum = 1;
	for (unsigned short int k = 1; k<=500; k++)
		sum += 4 * (4 * (k-1) * (k-1) + 6 * (k-1) + 3) + 12 * k;
	return sum; // 669171001
}

template <typename T>
T hcf(T a, T b)
{
	while (0 < a && 0 < b)
	{
		if (a < b)
		{
			b = b % a;
		}
		else
		{
			a = a % b;
		}
	}
	return a + b;
}

unsigned short int getIfPower(unsigned short int a)
{
	// find all the prime divisors and their powers
	unsigned short int divisor = 1;
	vector<unsigned short int> powers;
	bool divisible = true;
	while (1 < a && divisible)
	{
		divisor++;
		while (a % divisor != 0 && divisor <= sqrt(a))
			divisor++;
		if (a % divisor != 0)
		{
			divisible = false;
		}
		else
		{
			unsigned short int power = 0;
			while (a % divisor == 0)
			{
				a = a / divisor;
				power++;
			}
			powers.push_back(power);
		}
	}
	if (divisible)
	{
		// calculate highest common factor of the powers
		unsigned short int commonHCF = powers.front();
		for (auto item = powers.begin(); item != powers.end(); item++)
			commonHCF = hcf(commonHCF, *item);
		return commonHCF;
	}
	else
	{
		return 1;
	}
}

unsigned long int problem29()
{
	unsigned long int countUnique = 0;
	for (unsigned short int a = 2; a <= 100; a++)
	{
		unsigned short int power = getIfPower(a);
		//cout << "getIfPower " << a << " = " << power << endl;
		if (1 < power)
		{
			for (unsigned short int b = 2; b <= 100; b++)
			{
				bool newPower = true;
				for (unsigned short int k = 1; k < power && newPower; k++)
					if (power * b % k == 0 && power * b / k <= 100)
						newPower = false;
				if (newPower)
					countUnique++;
			}
		}
		else
		{
			countUnique += 99;
		}
	}
	return countUnique; // 9183
}

unsigned int problem30()
{
	unsigned short int power5[10];
	for (unsigned short int i = 0; i < 10; i++)
		power5[i] = i * i * i * i * i;

	unsigned int MAX = 1000000;

	unsigned short int digits[6];
	fill(digits, digits+6, 0);
	unsigned int n = 100;
	digits[2] = 1;

	unsigned int sum = 0;
	while (++n < MAX)
	{
		// increment the array representation of "n"
		unsigned short int position = 0;
		while (digits[position] == 9)
		{
			digits[position] = 0;
			position++;
		}
		digits[position]++;

		// calculate sum of powers 5
		unsigned int sum5power = 0;
		for (unsigned short int j = 0; j < 6; j++)
			sum5power += power5[ digits[j] ];

		// add
		if (sum5power == n)
			sum += n;
	}

	return sum; // 443839
}

unsigned int problem31()
{
	vector<unsigned short int> coin;
	coin.push_back(200);
	coin.push_back(100);
	coin.push_back(50);
	coin.push_back(20);
	coin.push_back(10);
	coin.push_back(5);
	coin.push_back(2);
	coin.push_back(1);
	unsigned short int n = coin.size();
	unsigned short int *wallet = new unsigned short int[n];
	fill(wallet, wallet + n, 0);

	wallet[0] = 1; // two pounds
	unsigned int cnt = 1;
	bool work = true;
	while (work)
	{
		unsigned short int lastToDecrement = n-2;
		while (wallet[lastToDecrement] == 0 && 0 < lastToDecrement)
			lastToDecrement--;
		if (0 == wallet[lastToDecrement]) //(lastToDecrement < 0)
		{
			work = false;
		}
		else
		{
			wallet[lastToDecrement]--;
			unsigned short int toAdd = coin.at(lastToDecrement);
			// also remove all smaller coins
			for (unsigned short int j = lastToDecrement+1; j < n; j++)
			{
				toAdd += wallet[j] * coin.at(j);
				wallet[j] = 0;
			}

			unsigned short int nextCoin = lastToDecrement;
			while (0 < toAdd)
			{
				nextCoin++;
				wallet[nextCoin] += toAdd / coin.at(nextCoin);
				toAdd = toAdd % coin.at(nextCoin);
			}
			
			cnt++;
		}
	}
	return cnt; // 156 //73682 // 
}

vector<unsigned short int> pr32_fixed;
set<unsigned short int> pr32_available;
set<unsigned short int> pr32_result;
unsigned int pr32_count = 0;

void pr32_run()
{
	int n = pr32_available.size();
	if (n == 0)
	{
		//pr32_count++;
		//if (pr32_count == 1 || pr32_count > 362879 || pr32_count % 50000 == 0)
		//	cout << "(" << pr32_count << ") " << 1000*pr32_fixed.at(5) + 100*pr32_fixed.at(6) + 10*pr32_fixed.at(7) + pr32_fixed.at(8)
		//		<< endl;

		unsigned short int last4 =
			1000*pr32_fixed.at(5) + 100*pr32_fixed.at(6) + 10*pr32_fixed.at(7) + pr32_fixed.at(8);

		if (
			(10*pr32_fixed.at(0) + pr32_fixed.at(1)) *
			(100*pr32_fixed.at(2) + 10*pr32_fixed.at(3) + pr32_fixed.at(4)) ==
			last4
			)
			pr32_result.emplace(last4);

		if (pr32_fixed.at(0) *
			(1000*pr32_fixed.at(1) + 100*pr32_fixed.at(2) + 10*pr32_fixed.at(3) + pr32_fixed.at(4)) ==
			last4)
			pr32_result.emplace(last4);
	}
	else
	{
		for (int i=0; i<n; i++)
		{
			set<unsigned short int>::iterator element = pr32_available.begin();
			for (unsigned short int j=0; j<i; j++)
				element++;

			pr32_fixed.push_back(*element);
			pr32_available.erase(element);

			pr32_run();

			pr32_available.emplace(pr32_fixed.back());
			pr32_fixed.pop_back();
		}
	}
}

unsigned int problem32()
{
	for (unsigned short int i=1; i<10; i++)
		pr32_available.emplace(i);
	pr32_run();
	cout << pr32_result.size() << " numbers have been found." << endl;
	return accumulate(pr32_result.begin(), pr32_result.end(), 0); // 30424
//|	7 numbers have been found.
//Answer: 45228
}

unsigned int problem33()
{
	vector<unsigned short int> numerator, denominator;
	for (unsigned short int i = 1; i <= 9; i++)
		for (unsigned short int j = 1; j <= 9; j++)
			for (unsigned short int k = 1; k <= 9; k++)
			{
				if (j < k && ((i*10 + j)*k == j*(k*10 + i) || (10*j + i)*k == j*(10*i + k)))
				{
					numerator.push_back(j);
					denominator.push_back(k);
				}
			}
	cout << numerator.size() << " fractoions have been found." << endl;

	unsigned int nProduct = 1, dProduct = 1;
	for (auto ni = numerator.begin(); ni != numerator.end(); ni++)
		nProduct *= *ni;
	for (auto di = denominator.begin(); di != denominator.end(); di++)
		dProduct *= *di;

	return dProduct / hcf(nProduct, dProduct); // 100
}

unsigned int problem34()
{
	unsigned short int factorials[10];
	factorials[0] = 1;
	for (unsigned short int i = 1; i < 10; i++)
		factorials[i] = i * factorials[i-1];
	unsigned int const MIN = 10, MAX = 10000000;

	unsigned int n = MIN;
	vector<unsigned short int> digits;
	digits.push_back(0);
	digits.push_back(1);

	unsigned int sum = 0;
	while (++n < MAX)
	{
		// increment the array representation of "n"
		auto position = digits.begin();
		while (position != digits.end() && *position == 9)
		{
			*position = 0;
			position++;
		}
		if (position != digits.end())
		{
			(*position)++;
		}
		else
		{
			digits.push_back(1);
		}

		unsigned int sumDigFac = 0;
		for (position = digits.begin(); position != digits.end(); position++)
			sumDigFac += factorials[ *position ];

		// add
		if (sumDigFac == n)
			sum += n;
	}

	return sum; // 40730
}

set<unsigned int> getPrimes(unsigned int limit)
{
	set<unsigned int> prime;
	bool isPrime;
	set<unsigned int>::iterator primeItem;
	double sqrtI;
	for (unsigned int i = 2; i < limit; i++)
	{
		isPrime = true;
		sqrtI = sqrt(i);
		for (primeItem = prime.begin(); isPrime && primeItem != prime.end() && *primeItem <= sqrtI; primeItem++)
			if (i % *primeItem == 0)
				isPrime = false;
		if (isPrime)
			prime.emplace(i);
	}
	return prime;
}

bool isPrimeFast(unsigned int value)
{
	static set<unsigned int> primes;
	static unsigned int maxPrime = 1;
	unsigned int nextToCheck = maxPrime;
	while (maxPrime < value)
	{
		nextToCheck++;
		double sqrtIt = sqrt(nextToCheck);
		bool soFarPrime = true;
		for (auto i = primes.begin(); soFarPrime && i != primes.end() && *i <= sqrtIt; i++)
			if (nextToCheck % *i == 0)
				soFarPrime = false;
		if (soFarPrime)
		{
			primes.emplace(nextToCheck);
			maxPrime = nextToCheck;
		}
	}
	return primes.end() != primes.find(value);
}

unsigned short int problem35()
{
	 auto prime = getPrimes(1000000);
	 cout << prime.size() << " primes have been found." << endl;

	 unsigned short int cnt = 0;
	 for (auto i = prime.begin(); i != prime.end(); i++)
	 {
		 vector<unsigned short int> digits;
		 unsigned int value = *i;
		 while (0 < value)
		 {
			 digits.push_back(value % 10);
			 value = value / 10;
		 }

		 bool isCircle = true;
		 for (unsigned short int offset = 1; isCircle && offset < digits.size(); offset++)
		 {
			 unsigned int newValue = 0;
			 for (unsigned short int j=0; j < digits.size(); j++)
				 newValue += pow(10, j) * digits[(j + offset) % digits.size()];
			 //if (prime.count(newValue) == 0)
			 if (prime.find(newValue) == prime.end())
				 isCircle = false;
		 }
		 if (isCircle)
			 cnt++;
	 }
	 return cnt; // 55
}

bool isPalindrome(string s)
{
	return equal(s.begin(), s.begin() + s.size()/2, s.rbegin());
}

template<size_t _Bits>
bool isPalindrome(bitset<_Bits> bits)
{
	bool result = true;
	unsigned short int size = bits.size();
	while (0 < size && !bits.test(size - 1))
		size--;
	for (unsigned short int i = 0; result && i < size/2; i++)
		//result = bits.at(i) == bits.at(size - 1 - i);
		result = bits[i] == bits[size - 1 - i];
	return result;
}

bool isDblBasePalindrome(unsigned int value)
{
	bool result = true;
	if (isPalindrome( to_string(value) ))
	{
		//for (unsigned short int i = 0; result && i < str.length()/2; i++)
		//	result = str.at(i) == str.
		bitset<32> bits(value);
		return isPalindrome(bits);
	}
	else
	{
		result = false;
	}
	return result;
}

unsigned int problem36()
{
	unsigned int sum = 0;
	for (unsigned int i = 1; i < 1000000; i++)
		if (isDblBasePalindrome(i))
			sum += i;
	return sum; // 872187
}

unsigned int problem37()
{
	set<unsigned int> prime = getPrimes(1000000);
	unsigned int sum = 0, cnt = 0;
	for (auto i = prime.begin(); cnt < 11 && i != prime.end(); i++)
		if (7 < *i)
	{
		bool isIt = true;
		//unsigned short int power = log10(*i);
		unsigned int divisor = 10;
		while (isIt && divisor < *i)
		{
			isIt = (prime.find(*i % divisor) != prime.end()) &&
				(prime.find(*i / divisor) != prime.end());
			divisor *= 10;
		}

		if (isIt)
		{
			sum += *i;
			cnt++;
		}
	}
	cout << cnt << " truncatable primes have been found." << endl;
	return sum; // 748317
}

unsigned int getPandigMult(unsigned int base)
{
	set<unsigned short int> digits;
	unsigned short int multiplier = 0, value, nextDigit;

	while (digits.size() < 9)
	{
		value = base * ++multiplier;
		while (0 < value)
		{
			nextDigit = value % 10;
			if (0 < nextDigit && digits.find(nextDigit) == digits.end())
			{
				value = value / 10;
				digits.emplace(nextDigit);
			}
			else
			{
				return 0;
			}
		}
	}
	if (9 < digits.size())
	{
		return 0;
	}
	else
	{
		unsigned int result = 0;
		string s("");
		for (unsigned short int i = 1; i <= multiplier; i++)
			s = s + to_string(base * i);
		return stoi(s);
	}
}

unsigned int problem38()
{
	unsigned int maxPan = 0;
	//for (unsigned short int i = 92; i <= 98; i++)
	//	maxPan = max(maxPan, getPandigMult(i));
	//for (unsigned short int i = 921; i <= 987; i++)
	//	maxPan = max(maxPan, getPandigMult(i));
	for (unsigned short int i = 9213; i <= 9876; i++)
		maxPan = max(maxPan, getPandigMult(i));
	return maxPan; // 935218704 // 932718654
}

unsigned short int problem39()
{
	unsigned short int maxSols = 3, maxP = 120;
	for (unsigned short int p = 121; p<=1000; p++)
	{
		unsigned short int sols = 0;
		for (unsigned short int a = 1; a < p / 3; a++)
		{
			unsigned short int a2 = a * a, maxB = min(- a + 2 * p / 3, (p - a)/2);
			for (unsigned short int b = a + 1; b <= maxB; b++)
				if (a2 + b*b == (p - a - b)*(p - a - b))
					sols++;
		}
		if (maxSols < sols)
		{
			maxP = p;
			maxSols = sols;
		}

		/// /// ///
		if (p % 100 == 0)
			cout << "p > " << p << " ... " << endl;
		/// /// ///
	}
	return maxP; // 840
}

unsigned short int NumDigits(unsigned int x)  
{  
    return x < 10 ? 1 :   
    	(x < 100 ? 2 :   
    	(x < 1000 ? 3 :   
    	(x < 10000 ? 4 :   
    	(x < 100000 ? 5 :   
    	(x < 1000000 ? 6 :   
    	(x < 10000000 ? 7 :  
    	(x < 100000000 ? 8 :  
    	(x < 1000000000 ? 9 :  
    	10))))))));
}
unsigned int problem40()
{
	vector<unsigned int> d;
	d.push_back(1);
	for (unsigned short int di = 1; di <= 6; di++)
		d.push_back(10 * d.back());

	string nextNumStr;
	unsigned int nextNumInt = 0, product = 1, currentD = 0;
	auto nextD = d.begin();
	while (nextD != d.end())
	{
		nextNumInt++;
		nextNumStr = to_string(nextNumInt);
		//if (*nextD <= currentD + NumDigits(nextNumInt)) // nextNumStr.length()
		if (*nextD <= currentD + nextNumStr.length())
		{
			unsigned short int position = (*nextD) - currentD;
			product *= atoi(nextNumStr.substr(position - 1, 1).c_str());
			nextD++;
		}
		currentD += nextNumStr.length();
	}
	return product; //  210
}

unsigned int problem41()
{
	unsigned int limit = 100000, result = 0;
	auto prime = getPrimes(limit);
	unsigned short int numDigits = 9;

	while (result == 0)
	{
		vector<unsigned short int> digits;
		for (unsigned short int k = 1; k <= numDigits; k++)
			digits.push_back(k);
		do 
		{
			unsigned int current = 0;
			for (unsigned short int j = 0; j < numDigits; j++)
				current += digits.at(j) * pow(10, j);

			bool isPrime = true;
			for (auto i = prime.begin(); isPrime && (*i < sqrt(current)) && i != prime.end(); i++)
				if (current % *i == 0)
					isPrime = false;
			if (isPrime && result < current)
				result = current;
		}
		while (next_permutation(digits.begin(), digits.end()));

		numDigits--;
	}
	return result; // 6475321 // 7652413
}

unsigned short int problem42()
{
	const string LETTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
	unordered_map<char, unsigned short int> abc;
	for (unsigned short int i = 0; i < LETTERS.length(); i++)
		abc.emplace(LETTERS.at(i), i+1);

	set<unsigned short int> triangle;
	for (unsigned short int number = 1; number < 300; number++)
		triangle.emplace(number * (number + 1) / 2);

	unsigned short int cnt = 0;
	ifstream words("Problem42.txt");
	char buffer[256];
	if (words.is_open())
	{
		bool stop = false;
		while (!stop)
		{
			words.getline(buffer, 256, ',');
			if (0 == words.gcount())
			{
				stop = true;
			}
			else
			{
				unsigned short int lastComma = words.gcount() - 1;
				while ('"' != buffer[lastComma] && lastComma > 0)
					lastComma--;
				//string word(buffer, 1, lastComma - 1);
				if ('"' != buffer[0] || '"' != buffer[lastComma])
					throw new exception(buffer, words.gcount());

				unsigned short int value = 0;
				for (auto j = buffer+1; j != buffer + lastComma; j++)
					value += abc.at(*j);
				if (triangle.find(value) != triangle.end())
					cnt++;
			}
		}
		words.close();
	}
	return cnt; // 162
}

unsigned long long problem43()
{
	unsigned long long sum = 0;
	vector<unsigned short int> number;
	for (unsigned short int k=0; k<10; k++)
		number.push_back(k);

	do
	{
		if (0 < number.front() &&
			(100*number[1] + 10*number[2] + number[3]) % 2 == 0 &&
			(100*number[2] + 10*number[3] + number[4]) % 3 == 0 &&
			(100*number[3] + 10*number[4] + number[5]) % 5 == 0 &&
			(100*number[4] + 10*number[5] + number[6]) % 7 == 0 &&
			(100*number[5] + 10*number[6] + number[7]) % 11 == 0 &&
			(100*number[6] + 10*number[7] + number[8]) % 13 == 0 &&
			(100*number[7] + 10*number[8] + number[9]) % 17 == 0)
		{
			unsigned long long fuck = 1;
			for (auto i = number.rbegin(); i != number.rend(); i++)
			{
				sum += fuck * (*i);
				fuck *= 10;
			}
		}
	}
	while (next_permutation(number.begin(), number.end()));

	return sum; // 3905514 // 3810433002 // 16695334890
}

vector<unsigned int> pr44_all;
void addPentagonal()
{
	if (pr44_all.empty())
	{
		pr44_all.push_back(1);
	}
	else
	{
		pr44_all.push_back(pr44_all.back() + 3 * pr44_all.size() + 1);
	}
}
unsigned int getPentagonal(unsigned short int n)
{
	//for (unsigned short int i = pr44_all.size(); i <= n; i++)
	while (pr44_all.size() <= n)
		addPentagonal();
	return pr44_all.at(n);
}
bool isPentagonal(unsigned int number)
{
	while (pr44_all.back() < number)
		addPentagonal();
	return binary_search(pr44_all.begin(), pr44_all.end(), number);
}
unsigned int problem44()
{
	for (unsigned short int i=1; i < 65500; i++)
	{
		if (i % 1000 == 0)
			cout << i << "..." << endl;
		for (unsigned short int j=0; j < i; j++)
		{
			unsigned int a = getPentagonal(i), b = getPentagonal(i - 1 - j);
			if (isPentagonal(a - b) && isPentagonal(a + b))
				return a - b;
		}
	}
	return 0; // 5482660
}

unsigned int problem45()
{
	unsigned int T = 40755, H = 40755, P = 40755;
	unsigned short int tn = 285, hn = 143, pn = 165;
	while (true)
	{
		T += ++tn;
		while (P < T)
			P += 3 * pn++ + 1;
		while (H < T)
			H += 4 * hn++ + 1;
		if (P == T && T == H)
			return T; // 1533776805
	}
}

unsigned int problem46()
{
	unsigned int limit = 1000000;
	auto prime = getPrimes(limit);
	for (unsigned int i = 35; i < limit; i += 2)
		if (prime.find(i) == prime.end())
		{
			bool can = false;
			for (auto j=prime.begin(); !can && *j < i - 1 && j != prime.end(); j++)
			{
				unsigned short int rem = i - *j;
				if (rem % 2 == 0)
				{
					int a = (int) sqrt((double)rem / 2);
					if (rem / 2 == a * a)
						can = true;
				}
			}
			if (!can)
				return i;
		}
	return 0; // 5777
}

unsigned int problem47()
{	
	unsigned int limit = 1000000, consec = 0;
	auto prime = getPrimes(limit);
	for (unsigned int i = 647; i < limit; i++)
	{
		bool good = true;
		unsigned int rem = i;
		unsigned short int numPrimes = 0;
		for (auto j = prime.begin(); 1 < rem && good && j != prime.end(); j++)
			if (rem % *j == 0)
			{
				numPrimes++;
				if (4 < numPrimes)
					good = false;
				while (rem % *j == 0)
					rem = rem / *j;
			}
		if (good && numPrimes == 4)
			consec++;
		else
			consec = 0;
		if (consec == 4)
			return i-3;
	}
	return 0; // 134043
}

template <typename T>
T addByMod(T a, T b, T mod)
{
	//return ((a % mod) + (b % mod)) % mod;
	return (a + b) % mod;
}

template <typename T>
T multByMod(T a, T b, T mod)
{
	a = a % mod;
	b = b % mod;
	return (a * b) % mod;
}

unsigned long long problem48()
{
	const unsigned long long MOD = 10000000000ULL;
	unsigned long long result = 0ULL;
	for (unsigned short int i=1; i<1000; i++)
	{
		unsigned long long newAdd = 1;
		for (unsigned short int j=0; j<i; j++)
			newAdd = multByMod(newAdd, (unsigned long long)i, MOD);
		result = addByMod(newAdd, result, MOD);
	}
	return result; // 9110846700
}

unsigned int pr49_traverse(set<unsigned short int> &digits, vector<unsigned short int> &chosen)
{
	static unsigned int cnt = 0;

	if (chosen.size() == 4)
	{
		vector<unsigned short int> toPermutate(chosen.begin(), chosen.end()), result;
		//cout << "[" << toPermutate[0] << toPermutate[1] << toPermutate[2] << toPermutate[3] << "]" << endl;
		cnt++;
		sort(toPermutate.begin(), toPermutate.end());

		do
		{
			if (0 < toPermutate[0])
			{
				unsigned short int value = 1000*toPermutate[0] + 100*toPermutate[1] +
					10*toPermutate[2] + toPermutate[3];
				if (isPrime(value))
					result.push_back(value);
			}
		}
		while (next_permutation(toPermutate.begin(), toPermutate.end()));


		if (3 <= result.size())
		{
			sort(result.begin(), result.end());
			for (auto i1 = result.begin(); i1 != result.end(); i1++)
				for (auto i2 = i1+1; i2 != result.end(); i2++)
					for (auto i3 = i2+1; i3 != result.end(); i3++)
						if (*i2 - *i1 == *i3 - *i2)
							cout << "Found! " << *i1 << *i2 << *i3 << endl;
		}
	}
	else
	{
		set<unsigned short int>::const_iterator startFrom;
		//startFrom = chosen.empty()
		//	? digits.cbegin()
		//	: (find(digits.cbegin(), digits.cend(), chosen.back()) + 1);
		if (chosen.empty())
		{
			startFrom = digits.cbegin();
		}
		else
		{
			startFrom = find(digits.cbegin(), digits.cend(), chosen.back());
			startFrom++;
		}

		for (set<unsigned short int>::iterator i=startFrom; i != digits.end(); i++)
		{
			//set<unsigned short int>::iterator pos = digits.begin();
			//for (unsigned short int j=0; j<i; j++)
			//	pos++;

			chosen.push_back(*i);
			//digits.erase(pos);

				pr49_traverse(digits, chosen);

			//digits.emplace(chosen.back());
			chosen.pop_back();
		}
		//return "";
	}
	return cnt;
}

string problem49()
{
	//set<unsigned short int> digits;
	//while (digits.size() < 10)
	//	digits.emplace(digits.size());
	//vector<unsigned short int> chosen;
	//return to_string(
	//	pr49_traverse(digits, chosen)); // 148748178147

	unsigned short int digs[4];
	for (digs[0] = 0;		digs[0] < 10; digs[0]++)
	for (digs[1] = digs[0]; digs[1] < 10; digs[1]++)
	for (digs[2] = digs[1]; digs[2] < 10; digs[2]++)
	for (digs[3] = digs[2]; digs[3] < 10; digs[3]++)
	{
		vector<unsigned short int> toPermutate(digs, digs+4), result;

		do
		{
			if (0 < toPermutate[0])
			{
				unsigned short int value = 1000*toPermutate[0] + 100*toPermutate[1] +
					10*toPermutate[2] + toPermutate[3];
				if (isPrime(value))
					result.push_back(value);
			}
		}
		while (next_permutation(toPermutate.begin(), toPermutate.end()));


		if (3 <= result.size())
		{
			sort(result.begin(), result.end());
			for (auto i1 = result.begin(); i1 != result.end(); i1++)
				for (auto i2 = i1+1; i2 != result.end(); i2++)
					for (auto i3 = i2+1; i3 != result.end(); i3++)
						if (*i2 - *i1 == *i3 - *i2)
							cout << "Found! " << *i1 << *i2 << *i3 << endl;
		}
	}
	return 0; // 296962999629
}

unsigned int problem50()
{
	const unsigned int limit = 1000000;
	auto prime = getPrimes(limit);

	unsigned int maxPrime = 953, maxN = 21;
	for (auto startWith = prime.begin(); startWith != prime.end(); startWith++)
	{
		unsigned int sum = *startWith, n = 1;
		auto endWith = startWith;
		while (sum < limit && ++endWith != prime.end())
		{
			sum += *endWith;
			n++;
			if (prime.find(sum) != prime.end() && maxN < n)
			{
				maxPrime = sum;
				maxN = n;
			}
		}
	}
	return maxPrime; // 999983 // 997651
}

template <typename T>
T NimSum(T a, T b, T c)
{
	return (a ^ b ) ^ c;
}

unsigned int problem301()
{
	unsigned int cnt = 0, limit = 1 << 30;
	for (unsigned int i = 1; i <= limit; i++)
		if (0 == NimSum(i, 2*i, 3*i))
			cnt++;
	return cnt; // 2178308 // 2178309
}

unsigned int problem51()
{
	unsigned int result = 0;
	for (unsigned short int n = 6; n < 30 && result == 0; n++)
	{
		for (unsigned short int k = 1; k < n && result == 0; k++)
		{
			vector<unsigned short int> p;
			p.push_back(n-k);
			while (p.size() < k + 1)
				p.push_back(0);
			bool configurationOk = true;
			while (configurationOk && result == 0)
			{
				// process current configuration
				vector<unsigned short int> nk;
				while (nk.size() < n - k) nk.push_back(0);
				bool nextNK = true;
				while (nextNK)
				{
					unsigned short int countPrimes = 0;
					unsigned int lowestPrime = 0;
					unsigned short int failedCount = 0;
					// construct the number
					for (unsigned short int dgt = 1; dgt <= 9 && failedCount < 3; dgt++)
					{
						unsigned int number = 0;
						auto nkj = nk.begin();
						unsigned int tenth = 1;
						for (auto pi = p.begin(); pi != p.end(); pi++)
						{
							if (pi != p.begin())
							{
								number += tenth * dgt;
								tenth *= 10;
							}
							for (unsigned short int z = 0; z < *pi; z++)
							{
								number += tenth * (*nkj);
								tenth *= 10;
								nkj++;
							}
						}

						// check if prime
						if (isPrimeFast(number))
						{
							//cout << number << "..." << endl;
							countPrimes++;
							if (lowestPrime == 0 || number < lowestPrime)
								lowestPrime = number;
						}
						else
						{
							failedCount++;
						}
					}

					if (8 <= countPrimes)
					{
						result = lowestPrime;
						break;
					}

					// get next number
					auto nki = nk.begin();
					nextNK = false;
					while ((!nextNK) && nki != nk.end())
						if (9 == *nki)
						{
							(*nki) = 0;
							nki++;
						}
						else
						{
							(*nki) = 1 + (*nki);
							nextNK = true;
						}
				}

				// next configuration
				if (p.back() < n - k)
				{
					//unsigned short int pp;
					//for (pp = k-1; 0 == p.at(pp); pp--)
					//	;
					auto pp = p.end() - 2;
					while (0 == (*pp))
						pp--;
					//if (p.at(pp) == 0)
					if ((*pp) == 0)
						throw new exception("Unable to locate nonzero p.");
					//p.at(pp) = p.at(pp) - 1;
					(*pp) = (*pp) - 1;
					//unsigned short int preSum = 0;
					//for (auto j = p.begin(); j != p.begin() + pp + 1; j++)
					//	preSum += (*j);
					
					//auto nextP = p.begin() + pp + 1;
					pp++;
					(*pp) = n - k - std::accumulate(p.begin(), pp, 0);
					while ((++pp) != p.end())
						(*pp) = 0;
					if (std::accumulate(p.begin(), p.end(), 0) != n - k)
						throw new exception("Wrong configuration sum.");
				}
				else
				{
					// the last possible configuration has just been processed
					configurationOk = false;
				}
			}


		}
	}
	return result;
}

unordered_map<unsigned short int, unsigned short int> parseNumber(unsigned int value)
{
	unordered_map<unsigned short int, unsigned short int> result;
	while (0 < value)
	{
		unsigned short int nextDigit = value % 10;
		value = value / 10;

		auto pos = result.find(nextDigit);
		if (pos == result.end())
		{
			result[nextDigit] = 1;
		}
		else
		{
			pos->second = pos->second + 1;
		}
	}
	return result;
}

bool digitsMatch(const unordered_map<unsigned short int, unsigned short int> & first,
				 const unordered_map<unsigned short int, unsigned short int> & second)
{
	for (auto i = first.begin(); i != first.end(); i++)
	{
		auto found = second.find(i->first);
		if (found == second.end() || found->second != i->second)
			return false;
	}
	return first.size() == second.size();
}

unsigned int problem52()
{
	//unsigned int MIN_LIMIT = 100000, MAX_LIMIT = 166666;
	//while (true)
	//{
	//	for (unsigned int i = MIN_LIMIT; i <= MAX_LIMIT; i++)
	//	{
	//	}
	//	MIN_LIMIT *= 10;
	//	MAX_LIMIT *= 10;
	//}
	unsigned int x = 100000;
	while (true)
	{
		auto x2 = parseNumber(2*x);
		if (digitsMatch(x2, parseNumber(3*x)) &&
			digitsMatch(x2, parseNumber(4*x)) &&
			digitsMatch(x2, parseNumber(5*x)) &&
			digitsMatch(x2, parseNumber(6*x)))
		{
			return x;
		}
		else
		{
			x++;
		}
	}
}

unsigned int problem53()
{
	clock_t begin = clock();
	auto before = GetTickCount64();
	unsigned int summa = 0;
	unsigned short int n = 23, k = 10;
	unsigned long int Ckn = 1144066, LIMIT = 1000000, Ck_1n;
	while (n <= 100)
	{
		// reduce K as much as possible as long as C exceeds 1000000
		Ck_1n = (Ckn * k) / (n - k + 1);
		while (LIMIT < Ck_1n)
		{
			Ckn = Ck_1n;
			k--;
			Ck_1n = (Ckn * k) / (n - k + 1);
		}

		// get the number of Ckn that exceed 1000000 for the given N
		summa += n + 1 - 2*k;

		// go to the next N
		n++;
		Ckn = (Ckn * n) / (n - k);
	}
	clock_t end = clock();
	auto after = GetTickCount64();
	cout << (double(end-begin) / CLOCKS_PER_SEC) << endl;
	cout << (after - before) << endl;

	return summa; // 4153 // 4075
}

enum class Suit { Diamonds, Hearts, Clubs, Spades };
enum class Rank { Two=2, Three=3, Four=4, Five=5, Six=6, Seven=7, Eight=8, Nine=9, Ten=10,
				Jack=11, Queen=12, King=13, Ace=14 };
enum class Comb { HighCard, OnePair, TwoPairs, ThreeOfKind, Straight, Flush,
	FullHouse, FourOfKind, StraightFlush, RoyalFlush };

class PlayCard
{
public:
	Suit suit;
	Rank rank;
	PlayCard()
	{
	}
	PlayCard(Suit suitValue, Rank rankValue) : suit(suitValue), rank(rankValue)
	{
	}
	~PlayCard()
	{
	}
	static PlayCard Parse(char first, char second)
	{
		PlayCard result;
		switch (first)
		{
		case '2':
			result.rank = Rank::Two;
			break;
		case '3':
			result.rank = Rank::Three;
			break;
		case '4':
			result.rank = Rank::Four;
			break;
		case '5':
			result.rank = Rank::Five;
			break;
		case '6':
			result.rank = Rank::Six;
			break;
		case '7':
			result.rank = Rank::Seven;
			break;
		case '8':
			result.rank = Rank::Eight;
			break;
		case '9':
			result.rank = Rank::Nine;
			break;
		case 'T':
			result.rank = Rank::Ten;
			break;
		case 'J':
			result.rank = Rank::Jack;
			break;
		case 'Q':
			result.rank = Rank::Queen;
			break;
		case 'K':
			result.rank = Rank::King;
			break;
		case 'A':
			result.rank = Rank::Ace;
			break;
		default:
			throw new exception("Invalid rank.");
		}

		switch (second)
		{
		case 'C':
			result.suit = Suit::Clubs;
			break;
		case 'D':
			result.suit = Suit::Diamonds;
			break;
		case 'H':
			result.suit = Suit::Hearts;
			break;
		case 'S':
			result.suit = Suit::Spades;
			break;
		default:
			throw new exception("Invalid suit.");
		}

		return result;
	}
};

template <typename E>
typename std::underlying_type<E>::type to_underlying(E e) {
    return static_cast<typename std::underlying_type<E>::type>(e);
}

bool Consec(Rank first, Rank second)
{
	return second == Rank(1 + to_underlying(first));
}

// sort by rank
void SortHand(PlayCard * hand)
{
	for (unsigned short int i = 1; i<5; i++)
		for (unsigned short int j = 0; j < 5 - i; j++)
			if (hand[j+1].rank < hand[j].rank)
			{
				PlayCard temp = hand[j+1];
				hand[j+1] = hand[j];
				hand[j] = temp;
			}
}

// map by rank
map<Rank, unsigned short int> MapHand(PlayCard * hand)
{
	map<Rank, unsigned short int> ranks;
	for (unsigned short int k =0; k < 5; k++)
	{
		auto found = ranks.find(hand[k].rank);
		if (found != ranks.end())
		{
			(*found).second++;
		}
		else
		{
			ranks.emplace(hand[k].rank, 1);
		}
	}
	return ranks;
}

Comb DeriveCombination(PlayCard * hand)
{
	bool isFlush = (hand[0].suit == hand[1].suit) && (hand[1].suit == hand[2].suit) &&
		(hand[2].suit == hand[3].suit) && (hand[3].suit == hand[4].suit);

	SortHand(hand);

	bool isStraight = Consec(hand[0].rank, hand[1].rank) && Consec(hand[1].rank, hand[2].rank) && 
		Consec(hand[2].rank, hand[3].rank) && Consec(hand[3].rank, hand[4].rank);

	if (isFlush && isStraight)
		return hand[0].rank == Rank::Ten ? Comb::RoyalFlush : Comb::StraightFlush;

	// map by rank
	map<Rank, unsigned short int> ranks = MapHand(hand);

	unsigned short int maxCnt = 0;
	Rank maxRank;
	for (auto item : ranks)
		if (maxCnt < item.second)
		{
			maxRank = item.first;
			maxCnt = item.second;
		}

	if (maxCnt == 4)
		return Comb::FourOfKind;

	bool hasTwo = false;
	for (auto item2 : ranks)
		if (item2.second == 2)
			hasTwo = true;
	if (maxCnt == 3 && hasTwo)
		return Comb::FullHouse;

	if (isFlush)
		return Comb::Flush;

	if (isStraight)
		return Comb::Straight;

	if (maxCnt == 3)
		return Comb::ThreeOfKind;

	if (hasTwo)
	{
		unsigned short int pairCnt = 0;
		for (auto item3 : ranks)
			if (item3.second == 2)
				pairCnt++;
		return pairCnt < 2 ? Comb::OnePair : Comb::TwoPairs;
	}
	else
	{
		return Comb::HighCard;
	}
}

bool CombWin(Comb comb, PlayCard * alice, PlayCard * bob)
{
	switch (comb)
	{
	case Comb::Straight:
	case Comb::StraightFlush:
		SortHand(alice);
		SortHand(bob);
		return alice[0].rank > bob[0].rank;

	case Comb::HighCard:
	case Comb::OnePair:
	case Comb::TwoPairs:
	case Comb::ThreeOfKind:
	case Comb::FourOfKind:
		{
		auto aliceRanks = MapHand(alice);
		auto bobRanks = MapHand(bob);

		unsigned short int aMaxCnt = 0, bMaxCnt = 0;
		Rank aMaxRank, bMaxRank;
		for (auto item : aliceRanks)
			if (aMaxCnt < item.second || (aMaxCnt == item.second && aMaxRank < item.first))
			{
				aMaxRank = item.first;
				aMaxCnt = item.second;
			}
		for (auto item : bobRanks)
			if (bMaxCnt < item.second || (bMaxCnt == item.second && bMaxRank < item.first))
			{
				bMaxRank = item.first;
				bMaxCnt = item.second;
			}

		if (aMaxCnt != bMaxCnt)
			throw new exception("MaxCnt mismatch.");

		unsigned short int currentCnt = aMaxCnt;
		while (0 < currentCnt)
		{
			for (auto item : aliceRanks)
				if (item.second == currentCnt)
					aMaxRank = item.first;
			for (auto item : aliceRanks)
				if (item.second == currentCnt && aMaxRank < item.first)
					aMaxRank = item.first;

			for (auto item : bobRanks)
				if (item.second == currentCnt)
					bMaxRank = item.first;
			for (auto item : bobRanks)
				if (item.second == currentCnt && bMaxRank < item.first)
					bMaxRank = item.first;

			if (aMaxRank == bMaxRank)
			{
				currentCnt--;
			}
			else
			{
				return aMaxRank > bMaxRank;
			}
		}

		//if (aMaxRank == bMaxRank)
			throw new exception("Unable to determine winner - same rank.");

		//return aMaxRank > bMaxRank;
		}
	case Comb::Flush:
	case Comb::RoyalFlush:
	default:
		throw new exception("invalid comparison.");
	}
}

bool PokerWin(PlayCard * alice, PlayCard * bob)
{
	Comb aliceComb = DeriveCombination(alice);
	Comb bobComb = DeriveCombination(bob);
	
	return aliceComb == bobComb
		? CombWin(aliceComb, alice, bob)
		: aliceComb > bobComb;
}

unsigned int problem54()
{
	unsigned int result = 0;
	ifstream input("Problem54.txt");
	if (input.is_open())
	{
		char buffer[256];
		input.getline(buffer, 256);
		PlayCard *alice = new PlayCard[5], *bob = new PlayCard[5];
		while (input.gcount() >= 29)
		{
			for (unsigned short int i = 0; i<5; i++)
			{
				alice[i] = PlayCard::Parse(
					buffer[i * 3],
					buffer[i * 3 + 1]);
				bob[i] = PlayCard::Parse(
					buffer[15 + 3*i],
					buffer[15 + 3*i + 1]);
			}

			if (PokerWin(alice, bob))
				result++;

			input.getline(buffer, 256);
		}
		input.close();
	}
	else
	{
		throw new exception("Unable to open file.");
	}
	return result; // 376
}

unsigned int problem55()
{
	return 0;
}

int main()
{
	//cout << "Problem 16: " << problem16(2, 1000) << endl;
	//cout << "d(220) = " << getAmicable(220) << endl;
	//cout << "d(284) = " << getAmicable(284) << endl;
	try
	{
		cout << "Answer: " << problem55() << endl;
	}
	catch (exception e)
	{
		cout << e.what() << endl;
	}
	
	cin.get();
	return 0;
}