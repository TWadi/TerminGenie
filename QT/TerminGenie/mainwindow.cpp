#include "MainWindow.h"
#include <QVBoxLayout>
#include <QHBoxLayout>
#include <QFormLayout>
#include <QLabel>
#include <QComboBox>
#include <QPushButton>
#include <QCheckBox>
#include <QScrollArea>
#include <QDateTimeEdit>
#include <QSpacerItem>
#include <QSizePolicy>
#include <QGroupBox>
#include <QRadioButton>
#include <QApplication>

MainWindow::MainWindow(QWidget *parent) : QMainWindow(parent) {
    setupUI();
    setFixedSize(800, 600); // Set a fixed size for the window (adjust as needed)
}

void MainWindow::setupUI() {
    QScrollArea *scrollArea = new QScrollArea(this);
    QWidget *centralWidget = new QWidget(scrollArea);
    QVBoxLayout *mainLayout = new QVBoxLayout(centralWidget);

    // Header with logo and site name
    QLabel *headerLabel = new QLabel();
    headerLabel->setPixmap(QPixmap(":/new/prefix1/logo.png").scaled(QSize(200, 200), Qt::KeepAspectRatio));
    QLabel *siteNameLabel = new QLabel("Berlin.de");
    siteNameLabel->setStyleSheet("font-weight: bold; font-size: 20px; color: #004B9B;");

    QHBoxLayout *headerLayout = new QHBoxLayout();
    headerLayout->addWidget(headerLabel, 0, Qt::AlignLeft);
    headerLayout->addWidget(siteNameLabel, 0, Qt::AlignLeft);
    mainLayout->addLayout(headerLayout);

    // Form layout for labels and combo boxes
    QFormLayout *formLayout = new QFormLayout();
    formLayout->setRowWrapPolicy(QFormLayout::DontWrapRows);
    formLayout->setFieldGrowthPolicy(QFormLayout::FieldsStayAtSizeHint);
    formLayout->setFormAlignment(Qt::AlignLeft | Qt::AlignTop);
    formLayout->setLabelAlignment(Qt::AlignLeft);

    // ComboBox for "Staatsangehörigkeit"
    QComboBox *nationalityComboBox = new QComboBox();
    nationalityComboBox->addItem("Tunesien");
    formLayout->addRow(new QLabel("Staatsangehörigkeit"), nationalityComboBox);

    // ComboBox for "Anzahl der Personen"
    QComboBox *peopleComboBox = new QComboBox();
    peopleComboBox->addItem("eine Person");
    formLayout->addRow(new QLabel("Anzahl der Personen"), peopleComboBox);

    // ComboBox for "Leben Sie in Berlin zusammen mit einem Familienangehörigen"
    QComboBox *familyComboBox = new QComboBox();
    familyComboBox->addItem("nein");
    formLayout->addRow(new QLabel("Leben Sie in Berlin zusammen mit einem Familienangehörigen"), familyComboBox);

    mainLayout->addLayout(formLayout);

    // Buttons for "Aufenthaltstitel" section
    QVBoxLayout *buttonsLayout = new QVBoxLayout();
    buttonsLayout->setSpacing(10);

    QPushButton *applyResidenceTitleButton = new QPushButton("Aufenthaltstitel - beantragen");
    QPushButton *extendResidenceTitleButton = new QPushButton("Aufenthaltstitel - verlängern");
    QPushButton *transferResidenceTitleButton = new QPushButton("Aufenthaltstitel in einen neuen Pass übertragen");
    QPushButton *applySettlementPermitButton = new QPushButton("Niederlassungserlaubnis beantragen");
    QPushButton *asylCheckBox = new QPushButton("Aufenthaltsgestattung (Asyl) - verlängern");
    QPushButton *tolerationCheckBox = new QPushButton("Duldung - verlängern");

    // Style the buttons
    QString buttonStyle = "QPushButton { "
                          "background-color: #EFEFEF; "
                          "color: black; "
                          "margin: 5px; "
                          "padding: 10px; "
                          "border: 1px solid #CCC; "
                          "border-radius: 5px; "
                          "}";

    applyResidenceTitleButton->setStyleSheet(buttonStyle);
    extendResidenceTitleButton->setStyleSheet(buttonStyle);
    transferResidenceTitleButton->setStyleSheet(buttonStyle);
    applySettlementPermitButton->setStyleSheet(buttonStyle);
    asylCheckBox->setStyleSheet(buttonStyle);
    tolerationCheckBox->setStyleSheet(buttonStyle);

    buttonsLayout->addWidget(applyResidenceTitleButton);
    buttonsLayout->addWidget(extendResidenceTitleButton);
    buttonsLayout->addWidget(transferResidenceTitleButton);
    buttonsLayout->addWidget(applySettlementPermitButton);
    buttonsLayout->addWidget(asylCheckBox);
    buttonsLayout->addWidget(tolerationCheckBox);

    // Spacer to push buttons to the right
    QSpacerItem *spacer = new QSpacerItem(40, 20, QSizePolicy::Expanding, QSizePolicy::Minimum);
    buttonsLayout->addItem(spacer);

    mainLayout->addLayout(buttonsLayout);

    // 'Studium und Ausbildung' GroupBox with RadioButtons
    QGroupBox *studyGroupBox = new QGroupBox("Studium und Ausbildung");
    QVBoxLayout *studyLayout = new QVBoxLayout(studyGroupBox);

    // Sample RadioButtons for 'Studium und Ausbildung' section
    createAndAddRadioButton(studyLayout, "Aufenthaltserlaubnis für eine Berufsausbildung (§ 16a)");
    createAndAddRadioButton(studyLayout, "Aufenthaltserlaubnis zur Berufsausbildung (§ 16b)");

    mainLayout->addWidget(studyGroupBox);

    // 'Erwerbstätigkeit' GroupBox with CheckBoxes
    QGroupBox *employmentGroupBox = new QGroupBox("Erwerbstätigkeit");
    QVBoxLayout *employmentLayout = new QVBoxLayout(employmentGroupBox);

    // Sample CheckBoxes for 'Erwerbstätigkeit' section
    createAndAddCheckBox(employmentLayout, "Blaue Karte EU für Hochqualifizierte");
    createAndAddCheckBox(employmentLayout, "Another Employment Option");
    // ... Add other CheckBoxes for 'Erwerbstätigkeit' similarly ...

    mainLayout->addWidget(employmentGroupBox);

    // Ensure that all widgets and layouts are added to 'centralWidget' before setting it
    centralWidget->setLayout(mainLayout);

    // Set up the scroll area
    scrollArea->setWidget(centralWidget);
    scrollArea->setWidgetResizable(true);
    setCentralWidget(scrollArea);
}


void MainWindow::createAndAddRadioButton(QVBoxLayout *layout, const QString &text) {
    QRadioButton *radioButton = new QRadioButton(text);
    radioButton->setStyleSheet("QRadioButton { spacing: 5px; }");
    layout->addWidget(radioButton);
}

void MainWindow::createAndAddCheckBox(QVBoxLayout *layout, const QString &text) {
    QCheckBox *checkBox = new QCheckBox(text);
    checkBox->setStyleSheet("QCheckBox { spacing: 5px; }");
    layout->addWidget(checkBox);
}
