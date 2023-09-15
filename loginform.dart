import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:get/get.dart';
import 'package:keyencesatosbpl/menuform.dart';
import 'package:flutter_datawedge/flutter_datawedge.dart';
import 'package:flutter_datawedge/models/scan_result.dart';
import 'package:flutter_datawedge/models/scanner_status.dart';
import 'dart:async';
import 'dart:io';
import 'package:http/http.dart' as http;
import 'package:keyencesatosbpl/scanqrcode.dart';
import 'package:qr_code_scanner/qr_code_scanner.dart';

class LoginForm extends StatefulWidget {
  const LoginForm({super.key});

  @override
  State<LoginForm> createState() => _LoginFormState();
}

class _LoginFormState extends State<LoginForm> {
  // listener
  // late StreamSubscription<ScanResult> onScanResultListener;
  // late StreamSubscription<ScannerStatus> onScannerStatusListener;
  // //late StreamSubscription<ActionResult> onScannerEventListener;
  // List<ScanResult> scanResults = [];
  // String _lastStatus = '';
  late FlutterDataWedge fdw;
  //Future<void>? initScannerResult;

  //end listener
  var _datatextfield = TextEditingController();
  //var _datatextfield1 = TextEditingController();
  final FocusNode _focusNode = FocusNode();

  @override
  void initState() {
    // TODO: implement initState
    super.initState();
    //checkid(_datatextfield.text);

    WidgetsBinding.instance?.addPostFrameCallback((_) {
      // Gọi requestFocus() sau khi cấu trúc giao diện đã được xây dựng
      _focusNode.requestFocus();
      //_focusNode.nextFocus();
      Future.delayed(const Duration(seconds: 1), () {
        //_focusNode.unfocus();
        SystemChannels.textInput.invokeMethod('TextInput.hide');
      } );

    });
    //initScanner();
  }
  void initScanner()  {
    if (Platform.isAndroid) {
      fdw = FlutterDataWedge(profileName: 'FlutterDataWedge');
      // onScanResultListener = fdw.onScanResult
      //     .listen((result) => setState(() {
      //   scanResults.add(result);
      //
      //   if(_datatextfield.text != ''){
      //     //_datatextfield1.text = scanResults.last.data;
      //   }
      //   else{
      //     _datatextfield.text = scanResults.last.data;
      //   }
      // } ));
      // onScannerStatusListener = fdw.onScannerStatus.listen(
      //         (status) => setState(() => _lastStatus = status.status.toString()));
      // await fdw.initialize();
    }
  }
  @override
  void dispose() {
    // TODO: implement dispose
    //listener
    // onScanResultListener.cancel();
    // onScannerStatusListener.cancel();
    //endlistener
    _datatextfield.dispose();
    super.dispose();
  }
  @override
  Widget build(BuildContext context) {
    //Future.delayed(const Duration(), () => SystemChannels.textInput.invokeMethod('TextInput.hide'));
    final Size sizescreen = MediaQuery
        .of(context)
        .size;
    return
      Scaffold(
        appBar: AppBar(
          automaticallyImplyLeading: false,
          toolbarHeight: 40,
          title: const Center(child: Text('Login Screen', style: TextStyle(fontSize: 25))),
        ),
        body: SingleChildScrollView(
          child: Center(
              child: SizedBox(
                 width: sizescreen.width * 0.9,
                height: sizescreen.height * 0.85,
                child:  Column(
                    mainAxisAlignment: MainAxisAlignment.end,
                    children: [
                Row(
                children: [
                Container(
                    width: sizescreen.width * 0.1,
                    child: Text('ID:',style: TextStyle(fontSize: 24),)),
                const SizedBox(width: 5),
                Container(
                  width: sizescreen.width * 0.75,
                  child: TextField(
                     autofocus: false,
                    //keyboardType: TextInputType.none,
                    controller: _datatextfield,
                    decoration: const InputDecoration(
                      border: OutlineInputBorder(
                      ),
                      contentPadding: EdgeInsets.only(top: 0,bottom: 0,left: 5,right: 5)

                    ),
                    style: const TextStyle(fontSize: 25),
                    onSubmitted: (data) async {
                      //if(data == 'abc'){
                        Get.to(const MenuForm());
                        //List<Student> dt = await fetchStudent();
                        // dt.forEach((element) {
                        //   print(element.name);
                        // });

                      //}
                      // else{
                      //   _showMyDialog();
                      // }
                    },
                    onChanged: (data){
                      //Get.to(const MenuForm());
                      checkid(data);
                    },
                    focusNode: _focusNode,
                  ),
                )
                ],
                ),
                      const SizedBox(height: 20,),
                      ElevatedButton(onPressed: () async {

                        _datatextfield.text = '';
                        await getdataid();
                        },
                        style: ElevatedButton.styleFrom(padding: EdgeInsets.all(10)), child: const Text("Scan QR",style: TextStyle(fontSize: 20),),

                      ),

                      SizedBox(height: sizescreen.height*0.4,),
                      const Text('Published 14-09-2023',style: TextStyle(fontSize: 15,decoration: TextDecoration.underline)),
                      const SizedBox(height: 10,)
                ],
              )
              ),
          ),
        ),
    )
    ;
  }
  Future<void> getdataid() async {
    final data = await Get.to(const ScanQRCode());
    setState(() {
      if(data == null){
        _datatextfield.text =  '';
      }
      else{
        _datatextfield.text =  data;
        checkid(data);
      }

    });
  }
  void checkid(String iduser){
    setState(() {
      if(iduser == 'LINE- DIP 04'){
        // Future.delayed(const Duration(seconds: 2), () {
        //
        // });
        Get.to(const MenuForm());
      }
      else if(iduser == ''){
      }
      else{
        _showMyDialog();
      }
    });
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
                Text('Incorrect ID'),
              ],
            ),
          ),
          actions: <Widget>[
            TextButton(
              child: const Text('OK'),
              onPressed: () {
                _datatextfield.text = '';
                Navigator.of(context).pop();

              },
            ),
          ],
        );
      },
    );
  }
  Future<List<Student>> fetchStudent() async {
    final response = await http
        .get(Uri.parse('http://10.92.184.136:8000/API/GetStudent'),

    ).timeout(const Duration(seconds: 4000));
    if (response.statusCode == 200) {
      List<dynamic> list1 = jsonDecode(response.body);
      print('thanh cong');
      return list1.map((json) => Student.fromJson(json)).toList();
    } else {
      // If the server did not return a 200 OK response,
      // then throw an exception.
      print('loi');
      throw Exception('Failed to load album');
    }
  }

}
class Student {
  late int id;
  late String name;

  Student({required this.id, required this.name});

  factory Student.fromJson(Map<String, dynamic> json) {
    return Student(
      id: json['Id'],
      name: json['Name'],

    );
  }
}

