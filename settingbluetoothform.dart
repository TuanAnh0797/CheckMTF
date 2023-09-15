import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:get/get.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:flutter_bluetooth_serial/flutter_bluetooth_serial.dart';

class settingbluetooth extends StatefulWidget {
  const settingbluetooth({super.key});

  @override
  State<settingbluetooth> createState() => _settingbluetoothState();
}

class _settingbluetoothState extends State<settingbluetooth> {
  var _datatextfield = TextEditingController();
  BluetoothConnection? connection;
  bool get isConnected => (connection?.isConnected ?? false);
  @override
  void initState()  {
    // TODO: implement initState
    super.initState();
    loadSettings();
  }
  Future<void> loadSettings() async {
    final prefs = await SharedPreferences.getInstance();
    setState(() {
      _datatextfield.text = prefs.getString('mac') ?? ''; // Cập nhật giá trị TextField
    });
  }
  @override
  void dispose() {
    // TODO: implement dispose
    if (isConnected) {
      connection?.dispose();
      connection = null;
    }
    super.dispose();
  }
  @override
  Widget build(BuildContext context) {
    final Size sizescreen = MediaQuery
        .of(context)
        .size;
    return Scaffold(
      appBar: AppBar(
        title:  const Text('Setting Bluetooth',style: TextStyle(fontSize: 30)),
      ),
      body: SingleChildScrollView(
        child: Center(
          child: Container(
            padding: const EdgeInsets.only(top: 20),
            width: sizescreen.width * 0.95,
            height: sizescreen.height * 0.85,
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.end,
              children: [
                Row(
                  children: [
                    SizedBox(
                      width: sizescreen.width * 0.15,
                      child: const Text('MAC',style: TextStyle(fontSize: 18)),
                    ),
                    //SizedBox(width: 2,),
                    SizedBox(
                      width: sizescreen.width * 0.8,
                      child: TextField(
                        autofocus: false,
                        controller: _datatextfield,

                        decoration: const InputDecoration(
                          border: OutlineInputBorder(
                          ),
                          contentPadding: EdgeInsets.only(top: 0,bottom: 0,left: 5,right: 5),
                        ),
                        style: const TextStyle(fontSize: 15),
                      ),
                    )
                  ],
                ),
                const SizedBox(height: 15,),
                Row(
                  mainAxisAlignment: MainAxisAlignment.end,
                  children: [
                    Container(
                      color: Colors.green,
                      width: 150,
                      height: 40,
                      child: ElevatedButton(onPressed: () {
                        BluetoothConnection.toAddress(_datatextfield.text).then((_connection) {
                          connection = _connection;
                          _sendMessage();

                        }).catchError((error){
                          Get.snackbar(
                            'Notify',
                            'Cannot Connect Printer',
                            snackPosition: SnackPosition.TOP,
                          );
                        });
                      },
                          style: ElevatedButton.styleFrom(backgroundColor: Colors.green), child: const Text('Print Test',style: TextStyle(fontSize: 20))
                      ),
                    ),
                    const SizedBox(width: 10,),
                    SizedBox(
                      width: 100,
                      height: 40,
                      child: ElevatedButton(onPressed: () {

                        _showMyDialog();

                      }, child: const Text('Save',style: TextStyle(fontSize: 20),)),
                    ),

                  ],
                )
              ],
            ),
          ),
        ),
      ),

    );
  }
  void saveSettings(String key, String value) async {
    final prefs = await SharedPreferences.getInstance();
    prefs.setString(key, value);

  }
  Future<void> _showMyDialog() async {
    return showDialog<void>(
      context: context,
      barrierDismissible: false, // user must tap button!
      builder: (BuildContext context) {
        return AlertDialog(
          title: const Text('Notify'),
          content: const SingleChildScrollView(
            child: ListBody(
              children: <Widget>[
                Text('Confirm change'),
              ],
            ),
          ),
          actions: <Widget>[
            TextButton(
              child: const Text('Yes'),
              onPressed: () {
                saveSettings("mac",_datatextfield.text);
                Navigator.of(context).pop();

              },
            ),
            TextButton(
              child: const Text('No'),
              onPressed: () {
                loadSettings();
                Navigator.of(context).pop();
              },
            ),
          ],
        );
      },
    );
  }
  void _sendMessage() async {
    final data = await rootBundle.load('assets/StockCard_Sampling_old.prn');
    final content = String.fromCharCodes(data.buffer.asUint8List());

    if (content.length > 0) {
      try {
        connection!.output.add(Uint8List.fromList(utf8.encode(content + "\r\n")));
        await connection!.output.allSent;
      } catch (e) {
        // Ignore error, but notify state
        Get.snackbar(
          'Notify',
          e.toString(),
          snackPosition: SnackPosition.TOP,
        );
      }
    }
  }


}