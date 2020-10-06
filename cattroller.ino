

int pad1 = A1;
int pad2 = A2;
int roll = A3;
int button = A4;
int roll2 = A5;
int order[2];
int o = 0;


void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  pinMode(INPUT, pad1);
  pinMode(INPUT, pad2);
  pinMode(INPUT, roll);
  pinMode(INPUT, roll2);
  pinMode(INPUT, button);

}

void loop() {
  // put your main code here, to run repeatedly:

//postition normal: roll high roll2 low
//counter 90: roll high roll2 high
//counter 180: roll low roll2 high
//counter 270: roll low roll2 low

if(order[0]==2 && order[1] == 2){
   order[0] = 0;
   order[1] = 0;
   o = 0;
}

  if(analogRead(pad1)>100){
  
    order[o] = 1;
    
    if(o ==1){
       
      if(order[0] == 1 && order[1] == 2){
        if(analogRead(roll) >10 &&analogRead(roll2) == 0){
           
          Serial.print('d');
        }
        else if(analogRead(roll) >10 &&analogRead(roll2) > 10){
         
          Serial.print('w');
         
        }
        else if(analogRead(roll) ==0 &&analogRead(roll2) > 10){
         
          Serial.print('a');
         
        }
        else if(analogRead(roll) ==0 &&analogRead(roll2) == 0){
         
          Serial.print('s');
         
        }
      }

      if(order[0] == 2 && order[1] == 1){
         if(analogRead(roll) >10 &&analogRead(roll2) == 0){
           
          Serial.print('a');
        }
        else if(analogRead(roll) >10 &&analogRead(roll2) > 10){
         
          Serial.print('s');
         
        }
        else if(analogRead(roll) ==0 &&analogRead(roll2) > 10){
         
          Serial.print('d');
         
        }
        else if(analogRead(roll) ==0 &&analogRead(roll2) == 0){
         
          Serial.print('w');
         
        }
      }
      order[0] = 0;
      order[1] = 0;
      o = 0;
    }
    else{
 
    o++;
    }
  }

  ///////////////
   if(analogRead(pad2)>100){
   
    order[o] = 2;
   if(o ==1){
       
      if(order[0] == 1 && order[1] == 2){
        if(analogRead(roll) >10 &&analogRead(roll2) == 0){
           
          Serial.print('d');
        }
        else if(analogRead(roll) >10 &&analogRead(roll2) > 10){
         
          Serial.print('w');
         
        }
        else if(analogRead(roll) ==0 &&analogRead(roll2) > 10){
         
          Serial.print('a');
         
        }
        else if(analogRead(roll) ==0 &&analogRead(roll2) == 0){
         
          Serial.print('s');
         
        }
      }

      if(order[0] == 2 && order[1] == 1){
         if(analogRead(roll) >10 &&analogRead(roll2) == 0){
           
          Serial.print('a');
        }
        else if(analogRead(roll) >10 &&analogRead(roll2) > 10){
         
          Serial.print('s');
         
        }
        else if(analogRead(roll) ==0 &&analogRead(roll2) > 10){
         
          Serial.print('d');
         
        }
        else if(analogRead(roll) ==0 &&analogRead(roll2) == 0){
         
          Serial.print('w');
         
        }
      }
      order[0] = 0;
      order[1] = 0;
      o = 0;
    }
    else{
 
    o++;
    }
  }
  //////

  if(analogRead(button) > 0){
    //keyboard action
    Serial.println();
  }


delay(300);

}
