<template>
    <div class="main">
        <div v-if="capturedImage !== null" class="displayFlex">
            <img class="imgPreview" :src="capturedImage"  alt=""/>
        </div>
        <video v-show="capturedImage === null" ref="video" class="video" width="100%" height="100%" playsinline autoplay></video>
        <div class="footer">
            <template v-if="capturedImage === null">
                <div class="icon" style="width: 33%; text-align: center;" @click="$router.push('/')"><v-icon name="backward" scale="1.5" /></div>
                <div @click="captureImg()" class="circle"></div>
                <div class="icon" style="width: 33%; text-align: center;" @click="changeCamera()"><v-icon name="random" scale="1.5" /></div>
            </template>
            <template v-else>
                <div class="icon" style="width: 33%; text-align: center;" @click="cancelTakingImage()"><v-icon name="undo-alt" scale="1.5" /></div>
                <div class="icon" style="width: 33%; text-align: center;" @click="sendFile()"><v-icon name="paper-plane" scale="1.5" /></div>
            </template>
        </div>
    </div>
</template>

<script lang="ts">
    import {Component, Vue} from 'vue-property-decorator';
    // @ts-ignore
    import $ from 'jquery';
    import axios from 'axios';

    @Component
    export default class Camera extends Vue {
        private videoElement: any;
        private videoOptions: any[] = [];
        private selectedIndex: any = null;
        private capturedImage: string | null = null;

        private windowData: { x: number, y: number } = { x: 0, y: 0 };
        private async cancelTakingImage() {
            this.capturedImage = null;
            await this.startMain();
        }
        private async sendFile() {

            const data = (this.capturedImage as string).substring(22);
            const response = await axios.post('https://hyapi.v-rp.eu/api/Classification', {
              base64: data,
            });

            if (response.status === 200) {
                console.log(response.data);
                this.$router.push({ name: 'summary', params: response.data });
            }
        }
        private devicesFound(devices: any) {
            devices.forEach((device: any) => {
                if (device.kind === 'videoinput') {
                    this.videoOptions.push(device);
                }
            });
        }
        private async changeCamera() {
            this.selectedIndex++;
            if (this.selectedIndex > this.videoOptions.length - 1) {
                this.selectedIndex = 0;
            }

            await this.startMain();
        }
        private captureImg() {
            const canvas = document.createElement('canvas');
            const video = $(this.videoElement).get(0);

            canvas.width = video.videoWidth;
            canvas.height = video.videoHeight;

            // @ts-ignore
            canvas.getContext('2d').drawImage(video, 0, 0, canvas.width, canvas.height);

            this.capturedImage = canvas.toDataURL();

            canvas.remove();

        }
        private errorHandler(error: any) {
            // tslint:disable-next-line:no-console
            console.log('navigator.getUserMedia error: ', error);
        }
        private async startMain() {
            // @ts-ignore
            if (window.stream) {
                // @ts-ignore
                window.stream.getTracks().forEach((track: any) => {
                    track.stop();
                });
            }

            if (this.videoOptions.length === 0) {
                return;
            }

            if (this.selectedIndex === null) {
                this.selectedIndex = 0;
            }

            const constraints = {
                video: {
                    deviceId: this.videoOptions[this.selectedIndex].deviceId,
                    aspectRatio: { exact: 1920 / 1080, ideal: 1920 / 1080 },
                },
            };

            const data = await navigator.mediaDevices.getUserMedia(constraints);

            // @ts-ignore
            window.stream = data;
            this.videoElement.srcObject = data;
            await this.videoElement.play();

            await this.refreshDevices();

        }
        private async refreshDevices() {
            const devices = await navigator.mediaDevices.enumerateDevices();
            this.devicesFound(devices);
        }
        private async mounted() {
            try {
                this.windowData = {
                    x: window.innerWidth,
                    y: window.innerHeight,
                };

                this.videoElement = this.$refs.video;
                await this.refreshDevices();
                await this.startMain();
            } catch (e) {
                // tslint:disable-next-line:no-console
                console.log(e);
            }


        }
    }
</script>

<style scoped lang="scss">

    .displayFlex {
        width: 100%;
        height: 100%;
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
        align-content: center;
    }

    .imgPreview {
        width: 100%;

    }

    .main {
        position: absolute;
        width: 100%;
        height: 100%;
        background-color: #181818;
    }

    h3 {
        margin: 40px 0 0;
    }

    ul {
        list-style-type: none;
        padding: 0;
    }

    li {
        display: inline-block;
        margin: 0 10px;
    }

    a {
        color: #42b983;
    }

    .main {
        height: 100%;
        width: 100%;
    }

    .footer {

        position: fixed;
        width: 100%;
        bottom: 0;
        height: 10vh;
        background-image: linear-gradient(rgba(0, 0, 0, 0), rgba(0, 0, 0, 0.5));
        display: flex;
        justify-content: space-around;
        align-items: center;
        color: #FFFFFF;
        font-family: 'MavenPro-Regular', sans-serif;

        .circle {
            width: 3em;
            height: 3em;
            border-radius: 1.5em;

            background: #F2F1F2;
            opacity: 0.3;

            &:hover {
                opacity: 0.5;
            }

            &:active {
                opacity: 1;
            }
        }
    }
</style>
