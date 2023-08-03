<template>
  <!-- WRAPPER -->
<div>
                           <div class="row px-3">
                                <label for="" style="margin-top:-18px">User Name</label>
                              <input v-model="postBody.username" name="username" class="mb-4 form-styling" placeholder="" required />
                            </div>
                            <!-- <div class="row px-3">
                                 <label for="" style="margin-top:-18px">Password</label>
                                <input v-model="postBody.password" id="password-field"  type="password" name="password" class="mb-4 form-styling" placeholder="" required />
                                <span toggle="#password-field" class="fa fa-fw fa-eye field-icon toggle-password"></span>
                            </div> -->

                            <div class="form-group" style="margin-top:-18px">
                                <label for="" style="margin-top:-18px">Password</label>
                                        <div class="col-xs-12">
                                            <input v-if="showPassword" type="text" class="mb-4 form-styling" name="password"  v-model="postBody.password"/>
                                            <input v-else type="password" class="mb-4 form-styling" name="password"   v-model="postBody.password">
                                        </div>
                                        <div class="control field-icon "  style="margin-top:-60px">
                                            <button type="button" class="button mb-4 form-styling" @click="toggleShow">
                                                <span class="icon is-small is-right">
                                                    <i class="fas" :class="{ 'fa-eye-slash': showPassword, 'fa-eye': !showPassword }"></i>
                                                </span>
                                            </button>
                                        </div>
                            </div>
                            <!-- <div class="form-group" style="margin-top:-18px"> 
                            <div class="col-xs-12">
                                 <label for="" style="margin-top:-18px">Command</label>
                               <select class="form-control" v-model="postBody.commandid" name="command" @change="getship" required>
                                  <option v-for="comd in commandList" v-bind:value="comd.id" v-bind:key="comd.id">{{comd.commandName}}</option>
                               </select>
                            </div>
                            </div>
                        <div class="form-group">
                          <div class="col-xs-12 row px-3">
                              <label for="" style="margin-top:-18px">Ship</label>
                           <select class="mb-4 form-styling form-control" v-model="postBody.ship" name="ship" required>
                             <option v-for="ship in shipList" v-bind:value="ship.id" v-bind:key="ship.id">{{ship.shipName}}</option>
                            </select>
                            </div>
                        </div>
                      -->
                            <!-- <div class="row px-3 mb-4"> -->
                                <!-- <div class="custom-control custom-checkbox custom-control-inline">
                                    <input id="chk1" type="checkbox" name="chk" class="custom-control-input"> <label for="chk1" class="custom-control-label text-sm">Remember me</label>
                                </div> -->
                                <!-- <a href="#">Test Link</a> -->
                                <!-- <a href="/Account/ForgetPassword" class="ml-auto mb-0 text-sm">Forgot Password?</a> -->
                            <!-- </div> -->
                            <div class="row mb-3 px-3">
                                <button type="submit" class="btn-signin text-center">Login</button>
                            </div>
    
</div>
  </template>
  <script>
    export default {
    props: ["privacyLink", "cookieString"],
    data() {
        return {
            showPassword: false,
            hidden: false,
            commandList:null,
            shipList:null,
             postBody: {
                commandid:'',
                ship:'',
                username:'',
                password:'',
                appointment:'',

            }
        };
    },
    
     mounted () {
        axios
            .get('/api/command/getAllcommand')
            .then(response => {
                this.commandList = response.data
                })
        },
        computed: {
         buttonLabel() {
           return (this.showPassword) ? "Hide" : "Show";
           }
          },
         methods:{
        getship(){
           axios
        .get(`/api/command/getship/${this.postBody.commandid}`)
        .then(response=>(this.shipList=response.data
        ))},
        toggleShow() {
      this.showPassword = !this.showPassword;
    }
        },
    };
     
</script>