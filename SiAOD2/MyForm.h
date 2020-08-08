#pragma once
#include <chrono>


namespace Forma {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;

	/// <summary>
	/// —водка дл€ MyForm
	/// </summary>
	public ref class MyForm : public System::Windows::Forms::Form
	{
	public:
		MyForm(void)
		{
			InitializeComponent();
			//
			//TODO: добавьте код конструктора
			//
		}

	protected:
		/// <summary>
		/// ќсвободить все используемые ресурсы.
		/// </summary>
		~MyForm()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::TextBox^ txtRand;
	protected:

	private: System::Windows::Forms::Button^ btnGen;
	private: System::Windows::Forms::TextBox^ txtMass;
	protected:


	private: System::Windows::Forms::Label^ label1;
	private: System::Windows::Forms::GroupBox^ groupBox1;
	private: System::Windows::Forms::GroupBox^ groupBox2;
	private: System::Windows::Forms::Label^ label2;
	private: System::Windows::Forms::TextBox^ txtElem;

	private: System::Windows::Forms::Button^ btnFind;
	private: System::Windows::Forms::TextBox^ txtRes;
	private: System::Windows::Forms::TextBox^ txtMax;

	private: System::Windows::Forms::TextBox^ txtMin;

	private: System::Windows::Forms::Label^ label4;
	private: System::Windows::Forms::Label^ label3;
	private: System::Windows::Forms::Button^ btnSort;

	protected:

	private:
		/// <summary>
		/// ќб€зательна€ переменна€ конструктора.
		/// </summary>
		System::ComponentModel::Container ^components;

#pragma region Windows Form Designer generated code
		/// <summary>
		/// “ребуемый метод дл€ поддержки конструктора Ч не измен€йте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		void InitializeComponent(void)
		{
			this->txtRand = (gcnew System::Windows::Forms::TextBox());
			this->btnGen = (gcnew System::Windows::Forms::Button());
			this->txtMass = (gcnew System::Windows::Forms::TextBox());
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->groupBox1 = (gcnew System::Windows::Forms::GroupBox());
			this->txtMax = (gcnew System::Windows::Forms::TextBox());
			this->txtMin = (gcnew System::Windows::Forms::TextBox());
			this->label4 = (gcnew System::Windows::Forms::Label());
			this->label3 = (gcnew System::Windows::Forms::Label());
			this->groupBox2 = (gcnew System::Windows::Forms::GroupBox());
			this->btnFind = (gcnew System::Windows::Forms::Button());
			this->txtRes = (gcnew System::Windows::Forms::TextBox());
			this->txtElem = (gcnew System::Windows::Forms::TextBox());
			this->label2 = (gcnew System::Windows::Forms::Label());
			this->btnSort = (gcnew System::Windows::Forms::Button());
			this->groupBox1->SuspendLayout();
			this->groupBox2->SuspendLayout();
			this->SuspendLayout();
			// 
			// txtRand
			// 
			this->txtRand->Location = System::Drawing::Point(57, 50);
			this->txtRand->Name = L"txtRand";
			this->txtRand->Size = System::Drawing::Size(74, 20);
			this->txtRand->TabIndex = 0;
			this->txtRand->Text = L"10000";
			this->txtRand->KeyPress += gcnew System::Windows::Forms::KeyPressEventHandler(this, &MyForm::txtRand_KeyPress);
			// 
			// btnGen
			// 
			this->btnGen->Location = System::Drawing::Point(137, 48);
			this->btnGen->Name = L"btnGen";
			this->btnGen->Size = System::Drawing::Size(115, 23);
			this->btnGen->TabIndex = 1;
			this->btnGen->Text = L"Generate";
			this->btnGen->UseVisualStyleBackColor = true;
			this->btnGen->Click += gcnew System::EventHandler(this, &MyForm::btnGen_Click);
			// 
			// txtMass
			// 
			this->txtMass->Location = System::Drawing::Point(7, 19);
			this->txtMass->Multiline = true;
			this->txtMass->Name = L"txtMass";
			this->txtMass->Size = System::Drawing::Size(245, 82);
			this->txtMass->TabIndex = 2;
			this->txtMass->KeyPress += gcnew System::Windows::Forms::KeyPressEventHandler(this, &MyForm::txtMass_KeyPress);
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Location = System::Drawing::Point(4, 53);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(47, 13);
			this->label1->TabIndex = 3;
			this->label1->Text = L"Number:";
			// 
			// groupBox1
			// 
			this->groupBox1->Controls->Add(this->txtMax);
			this->groupBox1->Controls->Add(this->txtMin);
			this->groupBox1->Controls->Add(this->label4);
			this->groupBox1->Controls->Add(this->label3);
			this->groupBox1->Controls->Add(this->txtRand);
			this->groupBox1->Controls->Add(this->label1);
			this->groupBox1->Controls->Add(this->btnGen);
			this->groupBox1->Location = System::Drawing::Point(13, 13);
			this->groupBox1->Name = L"groupBox1";
			this->groupBox1->Size = System::Drawing::Size(259, 77);
			this->groupBox1->TabIndex = 4;
			this->groupBox1->TabStop = false;
			this->groupBox1->Text = L"Random";
			// 
			// txtMax
			// 
			this->txtMax->Location = System::Drawing::Point(188, 20);
			this->txtMax->Name = L"txtMax";
			this->txtMax->Size = System::Drawing::Size(64, 20);
			this->txtMax->TabIndex = 7;
			this->txtMax->Text = L"100";
			this->txtMax->KeyPress += gcnew System::Windows::Forms::KeyPressEventHandler(this, &MyForm::txtRand_KeyPress);
			// 
			// txtMin
			// 
			this->txtMin->Location = System::Drawing::Point(63, 20);
			this->txtMin->Name = L"txtMin";
			this->txtMin->Size = System::Drawing::Size(49, 20);
			this->txtMin->TabIndex = 6;
			this->txtMin->Text = L"0";
			this->txtMin->KeyPress += gcnew System::Windows::Forms::KeyPressEventHandler(this, &MyForm::txtRand_KeyPress);
			// 
			// label4
			// 
			this->label4->AutoSize = true;
			this->label4->Location = System::Drawing::Point(128, 23);
			this->label4->Name = L"label4";
			this->label4->Size = System::Drawing::Size(54, 13);
			this->label4->TabIndex = 5;
			this->label4->Text = L"Maximum:";
			// 
			// label3
			// 
			this->label3->AutoSize = true;
			this->label3->Location = System::Drawing::Point(6, 23);
			this->label3->Name = L"label3";
			this->label3->Size = System::Drawing::Size(51, 13);
			this->label3->TabIndex = 4;
			this->label3->Text = L"Minimum:";
			// 
			// groupBox2
			// 
			this->groupBox2->Controls->Add(this->btnSort);
			this->groupBox2->Controls->Add(this->btnFind);
			this->groupBox2->Controls->Add(this->txtRes);
			this->groupBox2->Controls->Add(this->txtElem);
			this->groupBox2->Controls->Add(this->txtMass);
			this->groupBox2->Controls->Add(this->label2);
			this->groupBox2->Location = System::Drawing::Point(13, 96);
			this->groupBox2->Name = L"groupBox2";
			this->groupBox2->Size = System::Drawing::Size(259, 264);
			this->groupBox2->TabIndex = 5;
			this->groupBox2->TabStop = false;
			this->groupBox2->Text = L"Result";
			// 
			// btnFind
			// 
			this->btnFind->Location = System::Drawing::Point(103, 105);
			this->btnFind->Name = L"btnFind";
			this->btnFind->Size = System::Drawing::Size(79, 23);
			this->btnFind->TabIndex = 8;
			this->btnFind->Text = L"Find element";
			this->btnFind->UseVisualStyleBackColor = true;
			this->btnFind->Click += gcnew System::EventHandler(this, &MyForm::btnFind_Click);
			// 
			// txtRes
			// 
			this->txtRes->Location = System::Drawing::Point(7, 134);
			this->txtRes->Multiline = true;
			this->txtRes->Name = L"txtRes";
			this->txtRes->ReadOnly = true;
			this->txtRes->Size = System::Drawing::Size(245, 124);
			this->txtRes->TabIndex = 0;
			// 
			// txtElem
			// 
			this->txtElem->Location = System::Drawing::Point(57, 107);
			this->txtElem->Name = L"txtElem";
			this->txtElem->Size = System::Drawing::Size(40, 20);
			this->txtElem->TabIndex = 7;
			this->txtElem->KeyPress += gcnew System::Windows::Forms::KeyPressEventHandler(this, &MyForm::txtRand_KeyPress);
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->Location = System::Drawing::Point(3, 110);
			this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(48, 13);
			this->label2->TabIndex = 6;
			this->label2->Text = L"Element:";
			// 
			// btnSort
			// 
			this->btnSort->Location = System::Drawing::Point(188, 105);
			this->btnSort->Name = L"btnSort";
			this->btnSort->Size = System::Drawing::Size(64, 23);
			this->btnSort->TabIndex = 9;
			this->btnSort->Text = L"Sort";
			this->btnSort->UseVisualStyleBackColor = true;
			this->btnSort->Click += gcnew System::EventHandler(this, &MyForm::btnSort_Click);
			// 
			// MyForm
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(284, 372);
			this->Controls->Add(this->groupBox2);
			this->Controls->Add(this->groupBox1);
			this->FormBorderStyle = System::Windows::Forms::FormBorderStyle::FixedSingle;
			this->Name = L"MyForm";
			this->Text = L"SiAOD2 - Search methods";
			this->groupBox1->ResumeLayout(false);
			this->groupBox1->PerformLayout();
			this->groupBox2->ResumeLayout(false);
			this->groupBox2->PerformLayout();
			this->ResumeLayout(false);

		}
#pragma endregion
	private: System::Void txtRand_KeyPress(System::Object^ sender, System::Windows::Forms::KeyPressEventArgs^ e) {
		if (Char::IsDigit(e->KeyChar) || e->KeyChar == 8)
			e->Handled = false;
		else e->Handled = true;
	}
private: System::Void btnGen_Click(System::Object^ sender, System::EventArgs^ e) {
	if (txtRand->Text->Length > 0 && 
		txtMin->Text->Length > 0 && 
		txtMax->Text->Length > 0) {
		Random ^random = gcnew Random;
		int number = Convert::ToInt32(txtRand->Text),
			min = Convert::ToInt32(txtMin->Text),
			max = Convert::ToInt32(txtMax->Text);
		txtMass->ResetText();
		//massive generation
		for (int i = 0; i < number; i++) {
			txtMass->AppendText((random->Next(min,max+1)).ToString());
			if(i+1 < number)
				txtMass->AppendText(" ");
		}
	}
}
private: System::Void txtMass_KeyPress(System::Object^ sender, System::Windows::Forms::KeyPressEventArgs^ e) {
	if (Char::IsDigit(e->KeyChar) || e->KeyChar == 8 || e->KeyChar == 32)
		e->Handled = false;
	else e->Handled = true;

}
private: System::Void btnFind_Click(System::Object^ sender, System::EventArgs^ e) {
	Sortmass(txtMass->Text->Split(' '));
	array<String^>^ a = txtMass->Text->Split(' ');
	int key = Convert::ToInt32(txtElem->Text);
	int n = a->Length;
	array<int>^ mas = gcnew array<int>(n);
	for (int i = 0; i < n; i++) {
		mas[i] = Convert::ToInt32(a[i]);
	}

	auto start = std::chrono::steady_clock::now();
	int res1 = InterpolSearch(mas, key)+1;
	auto end = std::chrono::steady_clock::now();
	auto time1_ns = std::chrono::duration_cast<std::chrono::nanoseconds>(end - start);

	start = std::chrono::steady_clock::now();
	int res2 = Array::IndexOf(mas,key)+1;
	end = std::chrono::steady_clock::now();
	auto time2_ns = std::chrono::duration_cast<std::chrono::nanoseconds>(end - start);

	if (res1 == -1 && res2 == -1) {
		txtRes->Text = "false";
	}
	else {
		txtRes->AppendText("\r\n\tInterpolation search\r\ntime(ns): " + time1_ns.count().ToString() + "\r\nPosition: " + res1 +
		"\r\n\tStandart search\r\ntime(ns): " + time2_ns.count().ToString() + "\r\nPosition: " + res2.ToString());
	}

	delete mas, a, start,end,res1,res2,time1_ns,time2_ns;
}
private: int InterpolSearch(array<int>^ mas, int key)
	   {
		   int mid, left = 0, right = mas->Length - 1;
		   
		   while (mas[left] <= key && mas[right] >= key)
		   {
			   mid = left + ((key - mas[left]) * (right - left)) / (mas[right] - mas[left]);
			   if (mas[mid] < key) left = mid + 1;
			   else if (mas[mid] > key) right = mid - 1;
			   else return mid;
		   }
		   if (mas[left].Equals(key)) return left;
		   else return -1;
	   }
private: void Sortmass(array<String^>^ mas) {
	int n = mas->Length;
	array<int>^ a = gcnew array<int>(n);
	for (int i = 0; i < n; i++)
	{
		a[i] = Convert::ToInt32(mas[i]);
	}
	Array::Sort(a);
	txtMass->ResetText();
	for (int i = 0; i < n; i++)
	{
		txtMass->AppendText(a[i].ToString());
		if (i + 1 < n)
			txtMass->AppendText(" ");
	}
}
private: System::Void btnSort_Click(System::Object^ sender, System::EventArgs^ e) {
	Sortmass(txtMass->Text->Split(' '));
}
};
}
