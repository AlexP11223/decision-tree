Simple decision tree induction and classification using ID3 algorithm with Gini index impurity measure. Shows calculations on each step. Allows only 2 classes (Y, G) and all instance parameters (x1, x2, ...) can have one of 2 values (c, d).

Created during ML course in university, following S. Russell and P. Norvig book "Artificial Intelligence - A Modern Approach" and lecture materials.

C#, VS 2015+.

Training and test sets are hardcoded in `Main` for simplicity.

Can be run using online compiler here: [https://dotnetfiddle.net/KNvfGJ](https://dotnetfiddle.net/KNvfGJ).

Output example resulting in this decision tree:

![](https://i.imgur.com/RoVj3m6.png)

```
e1: x1=c x2=c x3=d x4=d x5=d - Y
e2: x1=d x2=c x3=d x4=d x5=d - G
e3: x1=c x2=d x3=d x4=c x5=c - Y
e4: x1=c x2=c x3=d x4=c x5=d - Y
e5: x1=d x2=d x3=c x4=c x5=d - G
e6: x1=c x2=d x3=d x4=d x5=c - Y
e7: x1=d x2=d x3=c x4=d x5=d - Y
e8: x1=c x2=d x3=d x4=c x5=d - G
e9: x1=d x2=c x3=d x4=c x5=d - G
e10: x1=d x2=c x3=d x4=c x5=c - G

Selecting parameter for
e1: x1=c x2=c x3=d x4=d x5=d - Y
e2: x1=d x2=c x3=d x4=d x5=d - G
e3: x1=c x2=d x3=d x4=c x5=c - Y
e4: x1=c x2=c x3=d x4=c x5=d - Y
e5: x1=d x2=d x3=c x4=c x5=d - G
e6: x1=c x2=d x3=d x4=d x5=c - Y
e7: x1=d x2=d x3=c x4=d x5=d - Y
e8: x1=c x2=d x3=d x4=c x5=d - G
e9: x1=d x2=c x3=d x4=c x5=d - G
e10: x1=d x2=c x3=d x4=c x5=c - G
Gini(E) = 0,5
    Gini(E, x1) = 0,32
    P(x1=c) = 0,5 Gini(Ex1=c) = 0,32; P(x1=d) = 0,5 Gini(Ex1=d) = 0,32
GiniGain(x1) = 0,18
    Gini(E, x2) = 0,48
    P(x2=c) = 0,5 Gini(Ex2=c) = 0,48; P(x2=d) = 0,5 Gini(Ex2=d) = 0,48
GiniGain(x2) = 0,02
    Gini(E, x3) = 0,5
    P(x3=c) = 0,2 Gini(Ex3=c) = 0,5; P(x3=d) = 0,8 Gini(Ex3=d) = 0,5
GiniGain(x3) = 0
    Gini(E, x4) = 0,42
    P(x4=c) = 0,6 Gini(Ex4=c) = 0,44; P(x4=d) = 0,4 Gini(Ex4=d) = 0,38
GiniGain(x4) = 0,08
    Gini(E, x5) = 0,48
    P(x5=c) = 0,3 Gini(Ex5=c) = 0,44; P(x5=d) = 0,7 Gini(Ex5=d) = 0,49
GiniGain(x5) = 0,02
Selected x1

x1=c

Selecting parameter for
e1: x2=c x3=d x4=d x5=d - Y
e3: x2=d x3=d x4=c x5=c - Y
e4: x2=c x3=d x4=c x5=d - Y
e6: x2=d x3=d x4=d x5=c - Y
e8: x2=d x3=d x4=c x5=d - G
Gini(E) = 0,32
    Gini(E, x2) = 0,27
    P(x2=c) = 0,4 Gini(Ex2=c) = 0; P(x2=d) = 0,6 Gini(Ex2=d) = 0,44
GiniGain(x2) = 0,05
    Gini(E, x3) = 0,32
    P(x3=c) = 0 Gini(Ex3=c) = 1; P(x3=d) = 1 Gini(Ex3=d) = 0,32
GiniGain(x3) = 0
    Gini(E, x4) = 0,27
    P(x4=c) = 0,6 Gini(Ex4=c) = 0,44; P(x4=d) = 0,4 Gini(Ex4=d) = 0
GiniGain(x4) = 0,05
    Gini(E, x5) = 0,27
    P(x5=c) = 0,4 Gini(Ex5=c) = 0; P(x5=d) = 0,6 Gini(Ex5=d) = 0,44
GiniGain(x5) = 0,05
Selected x2

x2=c

All have class Y
e1: x3=d x4=d x5=d - Y
e4: x3=d x4=c x5=d - Y

x2=d

Selecting parameter for
e3: x3=d x4=c x5=c - Y
e6: x3=d x4=d x5=c - Y
e8: x3=d x4=c x5=d - G
Gini(E) = 0,44
    Gini(E, x3) = 0,44
    P(x3=c) = 0 Gini(Ex3=c) = 1; P(x3=d) = 1 Gini(Ex3=d) = 0,44
GiniGain(x3) = 0
    Gini(E, x4) = 0,33
    P(x4=c) = 0,666666666666667 Gini(Ex4=c) = 0,5; P(x4=d) = 0,333333333333333 Gini(Ex4=d) = 0
GiniGain(x4) = 0,11
    Gini(E, x5) = 0
    P(x5=c) = 0,666666666666667 Gini(Ex5=c) = 0; P(x5=d) = 0,333333333333333 Gini(Ex5=d) = 0
GiniGain(x5) = 0,44
Selected x5

x5=c

All have class Y
e3: x3=d x4=c - Y
e6: x3=d x4=d - Y

x5=d

All have class G
e8: x3=d x4=c - G

x1=d

Selecting parameter for
e2: x2=c x3=d x4=d x5=d - G
e5: x2=d x3=c x4=c x5=d - G
e7: x2=d x3=c x4=d x5=d - Y
e9: x2=c x3=d x4=c x5=d - G
e10: x2=c x3=d x4=c x5=c - G
Gini(E) = 0,32
    Gini(E, x2) = 0,2
    P(x2=c) = 0,6 Gini(Ex2=c) = 0; P(x2=d) = 0,4 Gini(Ex2=d) = 0,5
GiniGain(x2) = 0,12
    Gini(E, x3) = 0,2
    P(x3=c) = 0,4 Gini(Ex3=c) = 0,5; P(x3=d) = 0,6 Gini(Ex3=d) = 0
GiniGain(x3) = 0,12
    Gini(E, x4) = 0,2
    P(x4=c) = 0,6 Gini(Ex4=c) = 0; P(x4=d) = 0,4 Gini(Ex4=d) = 0,5
GiniGain(x4) = 0,12
    Gini(E, x5) = 0,3
    P(x5=c) = 0,2 Gini(Ex5=c) = 0; P(x5=d) = 0,8 Gini(Ex5=d) = 0,38
GiniGain(x5) = 0,02
Selected x2

x2=c

All have class G
e2: x3=d x4=d x5=d - G
e9: x3=d x4=c x5=d - G
e10: x3=d x4=c x5=c - G

x2=d

Selecting parameter for
e5: x3=c x4=c x5=d - G
e7: x3=c x4=d x5=d - Y
Gini(E) = 0,5
    Gini(E, x3) = 0,5
    P(x3=c) = 1 Gini(Ex3=c) = 0,5; P(x3=d) = 0 Gini(Ex3=d) = 1
GiniGain(x3) = 0
    Gini(E, x4) = 0
    P(x4=c) = 0,5 Gini(Ex4=c) = 0; P(x4=d) = 0,5 Gini(Ex4=d) = 0
GiniGain(x4) = 0,5
    Gini(E, x5) = 0,5
    P(x5=c) = 0 Gini(Ex5=c) = 1; P(x5=d) = 1 Gini(Ex5=d) = 0,5
GiniGain(x5) = 0
Selected x4

x4=c

All have class G
e5: x3=c x5=d - G

x4=d

All have class Y
e7: x3=c x5=d - Y

e19: x1=c x2=d x3=c x4=c x5=c - Y == Y
e20: x1=d x2=c x3=c x4=d x5=c - G == G
```
