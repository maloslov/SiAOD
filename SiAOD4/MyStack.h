#pragma once

namespace MyStack {
	using namespace System;

	ref class Node {
	public:
		Object^ obj;
		Node^ next;
		Node(Object^ o) { obj = o; }
	};

	ref class Stack {
	private:
		Node^ top;

	public:
		Stack() {}
		Object^ pop() {
			if (top == nullptr)
				return 0;

			Object^ res = top->obj;
			top = top->next;
			return res;
		}
		void push(Object^ obj) {
		{
			Node^ newNode = gcnew Node(obj);
			newNode->next = top;
			top = newNode;
		}
	}
		bool isEmpty() {
			return (top == nullptr);
		}
	};
}