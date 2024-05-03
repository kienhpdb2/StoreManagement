import React, { useEffect, useState } from 'react';
import { Button, Modal, Form, Input, Select, message, DatePicker } from 'antd';
import axios from '../../common/baseAxios';
import CreateTenantDto from './models/CreateTenantDto';
interface IOpenModalProps {
    open: boolean,
    closeForm: Function,
}
const AddModal: React.FC<IOpenModalProps> = (props: IOpenModalProps) => {
    const { open, closeForm } = props;
    const onFinish = (input: CreateTenantDto) => {
        axios.post(`Tenant`, input).then((res) => {
            let { data } = res;
            if (data && data.success === true) {
                message.success('Tạo cửa hàng thành công.')
                closeForm(true);
            }
            else {
                message.error(data.error)
            }
        })
    }
    return <>
        <Modal
            title="THÊM CỬA HÀNG"
            centered
            open={open}
            onOk={() => closeForm(false)}
            onCancel={() => closeForm(false)}
            footer={null}
            width={1000}
        >
            <Form
                name="basic"
                layout={'vertical'}
                style={{ maxWidth: 1000 }}
                onFinish={onFinish}
                autoComplete="off"
            >
                <Form.Item label="Tên cửa hàng" name="name" rules={[{ required: true, message: 'Vui lòng nhập tên cửa hàng!' }]}>
                    <Input placeholder='Tên cửa hàng' />
                </Form.Item>
                <Form.Item name="phone" label="Số điện thoại" rules={[{ required: true, message: 'Vui lòng nhập số điện thoại !' }]}>
                    <Input placeholder='Số điện thoại' />
                </Form.Item>
                <Form.Item label="Email" name="email" rules={[
                    { required: true, message: 'Vui lòng nhập email của bạn!' },
                    { type: 'email', message: 'Định dạng E-mail không đúng!' },
                ]}>
                    <Input placeholder='Email của bạn' />
                </Form.Item>
                <Form.Item
                    label="Mật khẩu"
                    name="password"
                    rules={[{ required: true, message: 'Vui lòng nhập mật khẩu của bạn!' },
                    {
                        min: 4,
                        message: 'Mật khẩu phải ít nhất 4 kí tự!',
                    }
                    ]}
                >
                    <Input.Password placeholder='Mật khẩu của bạn' />
                </Form.Item>
                <Form.Item name="storeOwnerName" label="Người đại diện" rules={[{ required: true, message: 'Vui lòng nhập người đại diện !' }]}>
                    <Input placeholder='Người đại diện' />
                </Form.Item>
                <Form.Item
                    label="Địa chỉ"
                    name="address"
                    rules={[
                        { required: true, message: 'Vui lòng nhập địa chỉ!' },
                    ]}
                >
                    <Input placeholder='Địa chỉ' />
                </Form.Item>
                <Form.Item
                    label="Lời giới thiệu"
                    name="description"
                >
                    <Input placeholder='Lời giới thiệu' />
                </Form.Item>
                <Form.Item wrapperCol={{ span: 24 }} style={{ textAlign: 'right' }}>
                    <Button type="primary" htmlType="submit" >
                        Lưu
                    </Button>
                </Form.Item>
            </Form>
        </Modal>
    </>
}
export default AddModal;