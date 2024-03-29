import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:keyencesatosbpl/loginform.dart';
import 'package:keyencesatosbpl/settingbluetoothform.dart';
import 'goodreceipttool.dart';
class MenuForm extends StatefulWidget {
  const MenuForm({super.key});

  @override
  State<MenuForm> createState() => _MenuFormState();
}

class _MenuFormState extends State<MenuForm> {
  @override
  Widget build(BuildContext context) {
    final Size sizescreen = MediaQuery
        .of(context)
        .size;
    return  Scaffold(
      appBar: AppBar(
        automaticallyImplyLeading: false,
        toolbarHeight: 40,
        title:  Row(
          mainAxisAlignment: MainAxisAlignment.spaceBetween,

          children: [
            const Text('Menu Screen',style: TextStyle(fontSize: 25)),
            TextButton(onPressed: (){
              _showMyDialog();
            }, child: const Icon(Icons.logout,color: Colors.white,))
          ],
        ),
      ),
      body: SingleChildScrollView(
        child: WillPopScope(
          onWillPop: () async {
            Get.snackbar(
              'Notify',
              'Please press the logout button!',
              snackPosition: SnackPosition.TOP,
            );

            // Trả về false để ngăn người dùng quay lại trang.
            return false;
          },
          child: Center(
            child: SizedBox(
                width: sizescreen.width * 0.9,
                height: sizescreen.height*0.85,
                child:  Column(
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    SizedBox(
                      width: sizescreen.width*0.7,
                      height: 50,
                      child: ElevatedButton(onPressed: (){
                        Get.to(const settingbluetooth());
                      }, child: const Text('Setting Bluetooth',style: TextStyle(fontSize: 20),)),
                    ),
                    const SizedBox(height: 20,),
                    SizedBox(
                      width: sizescreen.width*0.7,
                      height: 50,
                      child: ElevatedButton(onPressed: (){
                        Get.to(const goodreceipttool());
                      }, child: const Text('Good Receipt Tool',style: TextStyle(fontSize: 20),)),
                    ),
                    const SizedBox(height: 20,),
                    SizedBox(
                      width: sizescreen.width*0.7,
                      height: 50,
                      child: ElevatedButton(onPressed: (){

                      }, child: const Text('Good Receipt STD',style: TextStyle(fontSize: 20),)),
                    ),
                    const SizedBox(height: 20,),
                    SizedBox(
                      width: sizescreen.width*0.7,
                      height: 50,
                      child: ElevatedButton(onPressed: (){

                      }, child: const Text('Check Status',style: TextStyle(fontSize: 20),)),
                    ),
                  ],
                )),
          ),
        ),
      ),
    );
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
                Text('Confirm Logout'),
              ],
            ),
          ),
          actions: <Widget>[
            TextButton(
              child: const Text('Yes'),
              onPressed: () {
                //Get.to(const LoginForm());
                Get.offAll(()=>const LoginForm());
                //Navigator.of(context).pop();
              },
            ),
            TextButton(
              child: const Text('No'),
              onPressed: () {
                Navigator.of(context).pop();
              },
            ),
          ],
        );
      },
    );
  }

}

// class MenuForm extends StatelessWidget {
//   const MenuForm({super.key});
//
//   @override
//   Widget build(BuildContext context) {
//     final Size sizescreen = MediaQuery
//         .of(context)
//         .size;
//     return  Scaffold(
//       appBar: AppBar(
//         automaticallyImplyLeading: false,
//         title:  Row(
//           mainAxisAlignment: MainAxisAlignment.spaceBetween,
//
//           children: [
//             const Text('Menu Screen',style: TextStyle(fontSize: 30)),
//             TextButton(onPressed: (){
//               Get.to(const LoginForm());
//             }, child: const Icon(Icons.logout,color: Colors.white,))
//           ],
//         ),
//       ),
//       body: SingleChildScrollView(
//         child: WillPopScope(
//           onWillPop: () async {
//             Get.snackbar(
//               'Notify',
//               'Please press the logout button!',
//               snackPosition: SnackPosition.TOP,
//             );
//
//             // Trả về false để ngăn người dùng quay lại trang.
//             return false;
//           },
//           child: Center(
//             child: SizedBox(
//                 width: sizescreen.width * 0.9,
//                 height: sizescreen.height*0.85,
//                 child:  Column(
//                   mainAxisAlignment: MainAxisAlignment.center,
//                   children: [
//                     SizedBox(
//                       width: sizescreen.width*0.7,
//                       height: 50,
//                       child: ElevatedButton(onPressed: (){
//                           Get.to(const settingbluetooth());
//                       }, child: const Text('Setting Bluetooth',style: TextStyle(fontSize: 20),)),
//                     ),
//                     const SizedBox(height: 20,),
//                     SizedBox(
//                       width: sizescreen.width*0.7,
//                       height: 50,
//                       child: ElevatedButton(onPressed: (){
//                         Get.to(const goodreceipttool());
//                       }, child: const Text('Good Receipt Tool',style: TextStyle(fontSize: 20),)),
//                     ),
//                     const SizedBox(height: 20,),
//                     SizedBox(
//                       width: sizescreen.width*0.7,
//                       height: 50,
//                       child: ElevatedButton(onPressed: (){
//
//                       }, child: const Text('Good Receipt STD',style: TextStyle(fontSize: 20),)),
//                     ),
//                     const SizedBox(height: 20,),
//                     SizedBox(
//                       width: sizescreen.width*0.7,
//                       height: 50,
//                       child: ElevatedButton(onPressed: (){
//
//                       }, child: const Text('Check Status',style: TextStyle(fontSize: 20),)),
//                     ),
//                   ],
//                 )),
//           ),
//         ),
//       ),
//     );
//   }
//
// }

