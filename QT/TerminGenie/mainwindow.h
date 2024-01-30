#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
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


class MainWindow : public QMainWindow {
    Q_OBJECT

public:
    explicit MainWindow(QWidget *parent = nullptr);

private:
    void setupUI();
    void createAndAddRadioButton(QVBoxLayout *layout, const QString &text);
    void createAndAddCheckBox(QVBoxLayout *layout, const QString &text);
};

#endif // MAINWINDOW_H
