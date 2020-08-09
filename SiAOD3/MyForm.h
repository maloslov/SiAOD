#pragma once
#include <vector>
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
	private: System::Windows::Forms::GroupBox^ groupBox1;
	protected:




	private: System::Windows::Forms::TextBox^ txtRand;
	private: System::Windows::Forms::Label^ label1;
	private: System::Windows::Forms::Button^ btnGen;
	private: System::Windows::Forms::GroupBox^ groupBox2;

	private: System::Windows::Forms::Button^ btnFind;
	private: System::Windows::Forms::TextBox^ txtRes;
	private: System::Windows::Forms::TextBox^ txtElem;
	private: System::Windows::Forms::TextBox^ txtMass;
	private: System::Windows::Forms::Label^ label2;
	private: System::Windows::Forms::CheckBox^ checkSens;




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
			this->groupBox1 = (gcnew System::Windows::Forms::GroupBox());
			this->txtRand = (gcnew System::Windows::Forms::TextBox());
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->btnGen = (gcnew System::Windows::Forms::Button());
			this->groupBox2 = (gcnew System::Windows::Forms::GroupBox());
			this->btnFind = (gcnew System::Windows::Forms::Button());
			this->txtRes = (gcnew System::Windows::Forms::TextBox());
			this->txtElem = (gcnew System::Windows::Forms::TextBox());
			this->txtMass = (gcnew System::Windows::Forms::TextBox());
			this->label2 = (gcnew System::Windows::Forms::Label());
			this->checkSens = (gcnew System::Windows::Forms::CheckBox());
			this->groupBox1->SuspendLayout();
			this->groupBox2->SuspendLayout();
			this->SuspendLayout();
			// 
			// groupBox1
			// 
			this->groupBox1->Anchor = static_cast<System::Windows::Forms::AnchorStyles>(((System::Windows::Forms::AnchorStyles::Top | System::Windows::Forms::AnchorStyles::Left)
				| System::Windows::Forms::AnchorStyles::Right));
			this->groupBox1->Controls->Add(this->txtRand);
			this->groupBox1->Controls->Add(this->label1);
			this->groupBox1->Controls->Add(this->btnGen);
			this->groupBox1->Location = System::Drawing::Point(12, 12);
			this->groupBox1->Name = L"groupBox1";
			this->groupBox1->Size = System::Drawing::Size(260, 52);
			this->groupBox1->TabIndex = 6;
			this->groupBox1->TabStop = false;
			this->groupBox1->Text = L"Random";
			// 
			// txtRand
			// 
			this->txtRand->Location = System::Drawing::Point(56, 21);
			this->txtRand->Name = L"txtRand";
			this->txtRand->Size = System::Drawing::Size(74, 20);
			this->txtRand->TabIndex = 0;
			this->txtRand->Text = L"10000";
			this->txtRand->KeyPress += gcnew System::Windows::Forms::KeyPressEventHandler(this, &MyForm::txtRand_KeyPress);
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Location = System::Drawing::Point(3, 24);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(43, 13);
			this->label1->TabIndex = 3;
			this->label1->Text = L"Length:";
			// 
			// btnGen
			// 
			this->btnGen->Anchor = static_cast<System::Windows::Forms::AnchorStyles>(((System::Windows::Forms::AnchorStyles::Top | System::Windows::Forms::AnchorStyles::Left)
				| System::Windows::Forms::AnchorStyles::Right));
			this->btnGen->Location = System::Drawing::Point(136, 19);
			this->btnGen->Name = L"btnGen";
			this->btnGen->Size = System::Drawing::Size(117, 23);
			this->btnGen->TabIndex = 1;
			this->btnGen->Text = L"Generate text";
			this->btnGen->UseVisualStyleBackColor = true;
			this->btnGen->Click += gcnew System::EventHandler(this, &MyForm::btnGen_Click);
			// 
			// groupBox2
			// 
			this->groupBox2->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((((System::Windows::Forms::AnchorStyles::Top | System::Windows::Forms::AnchorStyles::Bottom)
				| System::Windows::Forms::AnchorStyles::Left)
				| System::Windows::Forms::AnchorStyles::Right));
			this->groupBox2->Controls->Add(this->checkSens);
			this->groupBox2->Controls->Add(this->btnFind);
			this->groupBox2->Controls->Add(this->txtRes);
			this->groupBox2->Controls->Add(this->txtElem);
			this->groupBox2->Controls->Add(this->txtMass);
			this->groupBox2->Controls->Add(this->label2);
			this->groupBox2->Location = System::Drawing::Point(12, 70);
			this->groupBox2->Name = L"groupBox2";
			this->groupBox2->Size = System::Drawing::Size(260, 311);
			this->groupBox2->TabIndex = 7;
			this->groupBox2->TabStop = false;
			this->groupBox2->Text = L"Result";
			// 
			// btnFind
			// 
			this->btnFind->Anchor = static_cast<System::Windows::Forms::AnchorStyles>(((System::Windows::Forms::AnchorStyles::Top | System::Windows::Forms::AnchorStyles::Left)
				| System::Windows::Forms::AnchorStyles::Right));
			this->btnFind->Location = System::Drawing::Point(173, 185);
			this->btnFind->Name = L"btnFind";
			this->btnFind->Size = System::Drawing::Size(80, 23);
			this->btnFind->TabIndex = 8;
			this->btnFind->Text = L"Find element";
			this->btnFind->UseVisualStyleBackColor = true;
			this->btnFind->Click += gcnew System::EventHandler(this, &MyForm::btnFind_Click);
			// 
			// txtRes
			// 
			this->txtRes->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((((System::Windows::Forms::AnchorStyles::Top | System::Windows::Forms::AnchorStyles::Bottom)
				| System::Windows::Forms::AnchorStyles::Left)
				| System::Windows::Forms::AnchorStyles::Right));
			this->txtRes->Location = System::Drawing::Point(7, 214);
			this->txtRes->Multiline = true;
			this->txtRes->Name = L"txtRes";
			this->txtRes->ReadOnly = true;
			this->txtRes->Size = System::Drawing::Size(246, 91);
			this->txtRes->TabIndex = 0;
			// 
			// txtElem
			// 
			this->txtElem->Location = System::Drawing::Point(56, 187);
			this->txtElem->Name = L"txtElem";
			this->txtElem->Size = System::Drawing::Size(111, 20);
			this->txtElem->TabIndex = 7;
			// 
			// txtMass
			// 
			this->txtMass->Anchor = static_cast<System::Windows::Forms::AnchorStyles>(((System::Windows::Forms::AnchorStyles::Top | System::Windows::Forms::AnchorStyles::Left)
				| System::Windows::Forms::AnchorStyles::Right));
			this->txtMass->Location = System::Drawing::Point(7, 19);
			this->txtMass->Multiline = true;
			this->txtMass->Name = L"txtMass";
			this->txtMass->Size = System::Drawing::Size(246, 128);
			this->txtMass->TabIndex = 2;
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->Location = System::Drawing::Point(2, 190);
			this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(54, 13);
			this->label2->TabIndex = 6;
			this->label2->Text = L"Substring:";
			// 
			// checkSens
			// 
			this->checkSens->AutoSize = true;
			this->checkSens->Checked = true;
			this->checkSens->CheckState = System::Windows::Forms::CheckState::Checked;
			this->checkSens->Location = System::Drawing::Point(7, 164);
			this->checkSens->Name = L"checkSens";
			this->checkSens->Size = System::Drawing::Size(123, 17);
			this->checkSens->TabIndex = 9;
			this->checkSens->Text = L"Using case sensitive";
			this->checkSens->UseVisualStyleBackColor = true;
			// 
			// MyForm
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(284, 393);
			this->Controls->Add(this->groupBox1);
			this->Controls->Add(this->groupBox2);
			this->Name = L"MyForm";
			this->Text = L"SiAOD3 - Substring search";
			this->groupBox1->ResumeLayout(false);
			this->groupBox1->PerformLayout();
			this->groupBox2->ResumeLayout(false);
			this->groupBox2->PerformLayout();
			this->ResumeLayout(false);

		}
#pragma endregion
	private: array<int>^ GetPrefix(String^ s) {
		int n = s->Length;
		array<int>^ res = gcnew array<int>(n);
		res[0] = 0;
		int index = 0;

		for (int i = 1; i < s->Length; i++) {
			while (index >= 0 && s[index] != s[i]) { index--; }
			index++;
			res[i] = index;
		}
		return res;
	}

	private: int FindSubstring(String^ pattern, String^ text) {
		int res = -1;
		array<int>^ pf = GetPrefix(pattern);
		int index = 0;

		for (int i = 0; i < text->Length; i++) {
			while (index > 0 && pattern[index] != text[i]){ 
				index = pf[index - 1]; 
			}
			if (pattern[index] == text[i]) 
				index++;
			if (index == pattern->Length){
				return res = i - index + 1;
			}
		}
		return res;
	}
	private: System::Void btnGen_Click(System::Object^ sender, System::EventArgs^ e) {
		if (txtRand->Text->Length > 0) {
			txtMass->ResetText();
			int n = Convert::ToInt32(txtRand->Text);
			Random^ rand = gcnew Random;
			String^ str = "";
			for (int i = 0; i < n; i++)
			{
				str += Convert::ToChar(rand->Next('A', 'z'));
			}
			txtMass->Text = str;
		}
	}
private: System::Void txtRand_KeyPress(System::Object^ sender, System::Windows::Forms::KeyPressEventArgs^ e) {
	if (Char::IsDigit(e->KeyChar) || e->KeyChar == 8)
		e->Handled = false;
	else e->Handled = true;
}
private: System::Void btnFind_Click(System::Object^ sender, System::EventArgs^ e) {
	if (txtElem->Text->Length > 0 && txtMass->Text->Length > 0) {
		String ^elem = txtElem->Text,
			^mass = txtMass->Text;
		if (!checkSens->Checked) {
			elem = elem->ToLowerInvariant();
			mass = mass->ToLowerInvariant();
		}
		//KMP substring search
		auto start = std::chrono::steady_clock::now();
		int res1 = (FindSubstring(elem, mass)+1);
		auto end = std::chrono::steady_clock::now();
		auto time1_ns = std::chrono::duration_cast<std::chrono::nanoseconds>(end - start);
		//standart substring search
		start = std::chrono::steady_clock::now();
		int res2 = (mass->IndexOf(elem)+1);
		end = std::chrono::steady_clock::now();
		auto time2_ns = std::chrono::duration_cast<std::chrono::nanoseconds>(end - start);
		//output
		if (res1 == 0 && res2 == 0)
			txtRes->AppendText("false");
		else
			txtRes->AppendText("\r\n\tSearch KMP\r\ntime(ns): " + time1_ns.count() + "\r\nposition: " + res1 +
				"\r\n\tStandart search\r\ntime(ns): " + time2_ns.count() + "\r\nposition: " + res2);
	}
}
};
}
